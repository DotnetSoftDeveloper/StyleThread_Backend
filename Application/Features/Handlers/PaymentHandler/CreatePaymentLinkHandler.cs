using Application.DTO;
using Application.DTO.Payment;
using Application.Features.Command.GenericCommands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Razorpay.Api;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.PaymentHandler
{
    internal class CreatePaymentLinkHandler(IRazorpayService _razorpayService, IMapper _mapper) : IRequestHandler<GenericCreateCommand<RazorpayPaymentLinkRequestDTO, GenericResponse<RazorpayPaymentLinkResponseDTO>>, GenericResponse<RazorpayPaymentLinkResponseDTO>>
    {
        public async Task<GenericResponse<RazorpayPaymentLinkResponseDTO>> Handle(GenericCreateCommand<RazorpayPaymentLinkRequestDTO, GenericResponse<RazorpayPaymentLinkResponseDTO>> request, CancellationToken cancellationToken)
        {
            try
            {
                // Set expiration time to 10 minutes from now
                var expireBy = DateTimeOffset.UtcNow.AddMinutes(20).ToUnixTimeSeconds();

                // Prepare Razorpay Payment Link request using the model
                var paymentLinkRequest = new RazorpayPaymentLinkRequest
                {
                    Amount = (double)request.Entity.Amount*100,
                    Currency = "INR",
                    AcceptPartial = false,
                    ExpireBy = (int)expireBy,
                    ReferenceId = request.Entity.ReferenceId,
                    Description = request.Entity.Description,
                    Customer = new CustomerDetail
                    {
                        Name = request.Entity.Customer.Name,
                        Contact = request.Entity.Customer.Contact,
                        Email = request.Entity.Customer.Email
                    },
                    Notify = new Notification
                    {
                        Sms = true,
                        Email = true
                    },
                    ReminderEnable = true,
                    CallbackUrl = request.Entity.CallbackUrl,
                    CallbackMethod = request.Entity.CallbackMethod
                };

                // Call the service
                var response = await _razorpayService.CreatePaymentLinkAsync(paymentLinkRequest);

                var responseData = _mapper.Map<RazorpayPaymentLinkResponseDTO>(response);

                // Return success response with the payment link details
                return new GenericResponse<RazorpayPaymentLinkResponseDTO>
                {
                    Success = true,
                    Content = responseData,
                    Message = "Payment link created successfully."
                };
            }
            catch (Exception ex)
            {
                // Handle exception and return failure response
                return new GenericResponse<RazorpayPaymentLinkResponseDTO>
                {
                    Success = false,
                    Error = ex.Message,
                    Message = "Failed to create payment link."
                };
            }
        }
    }
}
