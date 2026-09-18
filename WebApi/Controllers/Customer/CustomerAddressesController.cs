using Domain.Entities;
using InfraStructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Customer
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController(ApplicationDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAddresses(CancellationToken cancellationToken)
        {
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return Unauthorized(CreateResponse<string>(false, "Invalid session. Please sign in again."));
            }

            var addresses = await dbContext.CustomerAddresses
                .Where(address => address.CustomerId == customerId.Value)
                .OrderByDescending(address => address.IsDefault)
                .ThenByDescending(address => address.UpdatedAt)
                .Select(address => ToDto(address))
                .ToListAsync(cancellationToken);

            return Ok(CreateResponse(true, "Addresses fetched successfully.", addresses));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CustomerAddressDto addressDto, CancellationToken cancellationToken)
        {
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return Unauthorized(CreateResponse<string>(false, "Invalid session. Please sign in again."));
            }

            if (!IsValidAddress(addressDto))
            {
                return BadRequest(CreateResponse<string>(false, "Please fill all required address fields."));
            }

            var hasExistingAddress = await dbContext.CustomerAddresses
                .AnyAsync(address => address.CustomerId == customerId.Value, cancellationToken);

            if (addressDto.IsDefault || !hasExistingAddress)
            {
                await ClearDefaultAddress(customerId.Value, cancellationToken);
            }

            var now = DateTime.UtcNow;
            var address = new CustomerAddress
            {
                CustomerId = customerId.Value,
                FullName = addressDto.FullName.Trim(),
                Phone = addressDto.Phone.Trim(),
                Pincode = addressDto.Pincode.Trim(),
                Locality = addressDto.Locality.Trim(),
                AddressLine1 = addressDto.AddressLine1.Trim(),
                City = addressDto.City.Trim(),
                State = addressDto.State.Trim(),
                Landmark = addressDto.Landmark?.Trim(),
                AlternatePhone = addressDto.AlternatePhone?.Trim(),
                LocationTypeTag = NormalizeAddressType(addressDto.LocationTypeTag),
                IsDefault = addressDto.IsDefault || !hasExistingAddress,
                CreatedAt = now,
                UpdatedAt = now
            };

            dbContext.CustomerAddresses.Add(address);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Ok(CreateResponse(true, "Address saved successfully.", ToDto(address)));
        }

        [HttpPut("{addressId:int}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] CustomerAddressDto addressDto, CancellationToken cancellationToken)
        {
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return Unauthorized(CreateResponse<string>(false, "Invalid session. Please sign in again."));
            }

            if (!IsValidAddress(addressDto))
            {
                return BadRequest(CreateResponse<string>(false, "Please fill all required address fields."));
            }

            var address = await dbContext.CustomerAddresses
                .FirstOrDefaultAsync(item => item.CustomerAddressId == addressId && item.CustomerId == customerId.Value, cancellationToken);

            if (address == null)
            {
                return NotFound(CreateResponse<string>(false, "Address was not found."));
            }

            if (addressDto.IsDefault)
            {
                await ClearDefaultAddress(customerId.Value, cancellationToken);
            }

            address.FullName = addressDto.FullName.Trim();
            address.Phone = addressDto.Phone.Trim();
            address.Pincode = addressDto.Pincode.Trim();
            address.Locality = addressDto.Locality.Trim();
            address.AddressLine1 = addressDto.AddressLine1.Trim();
            address.City = addressDto.City.Trim();
            address.State = addressDto.State.Trim();
            address.Landmark = addressDto.Landmark?.Trim();
            address.AlternatePhone = addressDto.AlternatePhone?.Trim();
            address.LocationTypeTag = NormalizeAddressType(addressDto.LocationTypeTag);
            address.IsDefault = addressDto.IsDefault || address.IsDefault;
            address.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Ok(CreateResponse(true, "Address updated successfully.", ToDto(address)));
        }

        [HttpPut("{addressId:int}/default")]
        public async Task<IActionResult> SetDefaultAddress(int addressId, CancellationToken cancellationToken)
        {
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return Unauthorized(CreateResponse<string>(false, "Invalid session. Please sign in again."));
            }

            var address = await dbContext.CustomerAddresses
                .FirstOrDefaultAsync(item => item.CustomerAddressId == addressId && item.CustomerId == customerId.Value, cancellationToken);

            if (address == null)
            {
                return NotFound(CreateResponse<string>(false, "Address was not found."));
            }

            await ClearDefaultAddress(customerId.Value, cancellationToken);
            address.IsDefault = true;
            address.UpdatedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Ok(CreateResponse(true, "Default address updated successfully.", ToDto(address)));
        }

        [HttpDelete("{addressId:int}")]
        public async Task<IActionResult> DeleteAddress(int addressId, CancellationToken cancellationToken)
        {
            var customerId = GetCurrentCustomerId();
            if (customerId == null)
            {
                return Unauthorized(CreateResponse<string>(false, "Invalid session. Please sign in again."));
            }

            var address = await dbContext.CustomerAddresses
                .FirstOrDefaultAsync(item => item.CustomerAddressId == addressId && item.CustomerId == customerId.Value, cancellationToken);

            if (address == null)
            {
                return NotFound(CreateResponse<string>(false, "Address was not found."));
            }

            var wasDefault = address.IsDefault;
            dbContext.CustomerAddresses.Remove(address);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (wasDefault)
            {
                var nextAddress = await dbContext.CustomerAddresses
                    .Where(item => item.CustomerId == customerId.Value)
                    .OrderByDescending(item => item.UpdatedAt)
                    .FirstOrDefaultAsync(cancellationToken);

                if (nextAddress != null)
                {
                    nextAddress.IsDefault = true;
                    nextAddress.UpdatedAt = DateTime.UtcNow;
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            return Ok(CreateResponse<string>(true, "Address deleted successfully."));
        }

        private async Task ClearDefaultAddress(int customerId, CancellationToken cancellationToken)
        {
            var addresses = await dbContext.CustomerAddresses
                .Where(address => address.CustomerId == customerId && address.IsDefault)
                .ToListAsync(cancellationToken);

            foreach (var address in addresses)
            {
                address.IsDefault = false;
            }
        }

        private int? GetCurrentCustomerId()
        {
            var customerIdValue = User.FindFirst("userId")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.TryParse(customerIdValue, out var customerId) ? customerId : null;
        }

        private static bool IsValidAddress(CustomerAddressDto address)
        {
            return !string.IsNullOrWhiteSpace(address.FullName)
                && !string.IsNullOrWhiteSpace(address.Phone)
                && address.Phone.Trim().Length == 10
                && !string.IsNullOrWhiteSpace(address.Pincode)
                && address.Pincode.Trim().Length == 6
                && !string.IsNullOrWhiteSpace(address.Locality)
                && !string.IsNullOrWhiteSpace(address.AddressLine1)
                && !string.IsNullOrWhiteSpace(address.City)
                && !string.IsNullOrWhiteSpace(address.State);
        }

        private static string NormalizeAddressType(string? locationTypeTag)
        {
            var normalized = locationTypeTag?.Trim().ToUpperInvariant();
            return normalized == "WORK" ? "WORK" : "HOME";
        }

        private static CustomerAddressDto ToDto(CustomerAddress address)
        {
            return new CustomerAddressDto
            {
                CustomerAddressId = address.CustomerAddressId,
                CustomerId = address.CustomerId,
                FullName = address.FullName,
                Phone = address.Phone,
                Pincode = address.Pincode,
                Locality = address.Locality,
                AddressLine1 = address.AddressLine1,
                City = address.City,
                State = address.State,
                Landmark = address.Landmark,
                AlternatePhone = address.AlternatePhone,
                LocationTypeTag = address.LocationTypeTag,
                IsDefault = address.IsDefault
            };
        }

        private static GenericResponse<T> CreateResponse<T>(bool success, string message, T? content = default)
        {
            return new GenericResponse<T>
            {
                Success = success,
                Message = message,
                Content = content
            };
        }
    }
}
