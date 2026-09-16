using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Service;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.ProductHandler
{
    public class UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, CloudinaryService cloudinaryService) : IRequestHandler<GenericUpdateCommand<ProductDto, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly CloudinaryService _cloudinaryService = cloudinaryService;

        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<ProductDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null) throw new ArgumentNullException(nameof(request.Entity), "Product data cannot be null.");
            foreach (ProductVariantDto productVarient in request.Entity.ProductVariants)
            {
                if (productVarient.Image != null && productVarient.Image.Count > 0)
                {
                    var uploadedImageUrls = new List<string>();

                    foreach (var image in productVarient.Image) // Assuming Image is List<byte[]>
                    {
                        if (!image.StartsWith("http"))
                        {
                            string fileName = $"{Guid.NewGuid()}.jpg"; // Generate a unique file name
                            byte[] imageByteArray = Convert.FromBase64String(image);
                            var imageUrl = await _cloudinaryService.UploadImageAsync(imageByteArray, fileName);
                            if (imageUrl == null)
                            {
                                throw new InvalidOperationException("Error while processing the image.");
                            }
                            uploadedImageUrls.Add(imageUrl);
                        }
                        else
                        {
                            uploadedImageUrls.Add(image);
                        }

                    }
                    // Update the Image property with Cloudinary URLs
                    productVarient.Image = uploadedImageUrls; // Update your model to store image URLs instead of byte[]
                }
                if (productVarient.Price > 0 && productVarient.Discount >= 0)
                    {
                        // Calculate the discount amount
                        decimal discountAmount = (productVarient.Price * productVarient.Discount) / 100;
                        // Calculate the sale price after discount
                        decimal salePrice = productVarient.Price - discountAmount;

                        // Optionally, you can store or use the salePrice as needed
                        productVarient.SalePrice = salePrice;
                    }
                //}
            }
            // Process multiple images
            

            var product = _mapper.Map<Product>(request.Entity) ?? throw new InvalidOperationException("Mapping to Product entity failed.");

            await _unitOfWork.productVariantRepository.UpdateProductVariantAsync(product.ProductVariants, cancellationToken);
            await _unitOfWork.productVariantSize
                   .UpdateProductVariantSizeAsync( product?.ProductVariants?
                   .SelectMany(x => x.ProductVariantSizes ?? new List<ProductVariantSize>())
                   .ToList() ?? new List<ProductVariantSize>(), cancellationToken);



            await _unitOfWork.productRepository.UpdateAsync(product);
            await _unitOfWork.productRepository.SaveAsync(cancellationToken);

            return new GenericResponse<string>
            {
                Message = "Data Updated Successfully",
                Success = true
            };
        }

        private async Task<byte[][]> ProcessImagesAsync(IFormFile[] uploadImages, CancellationToken cancellationToken)
        {
            var imageBytesList = new List<byte[]>();

            foreach (var file in uploadImages)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream, cancellationToken); // Copy the IFormFile to the memory stream
                memoryStream.Position = 0; // Reset the position of the stream to the beginning

                using var image = Image.Load(memoryStream); // Load the image from the memory stream
                image.Mutate(x => x.Resize(300, 300)); // Resize the image to 300x300 pixels
                memoryStream.SetLength(0); // Clear the memory stream before saving
                image.Save(memoryStream, new PngEncoder()); // Save the resized image as PNG
                imageBytesList.Add(memoryStream.ToArray()); // Convert to byte array and add to the list
            }

            return imageBytesList.ToArray(); // Return the array of byte arrays
        }
    }
}
