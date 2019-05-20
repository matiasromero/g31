using HomeSwitchHome.API.Contracts.V1;
using HomeSwitchHome.Application;
using HomeSwitchHome.Application.Models.Products;
using HomeSwitchHome.Application.Services.Users;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.IO;
using System.Linq;
using HomeSwitchHome.Application.Models.Residences;
using HomeSwitchHome.Application.Services.Residences;
using ImagesUtils = HomeSwitchHome.API.Utils.ImagesUtils;

namespace HomeSwitchHome.API.Controllers.V1
{
    public class ResidencesController : ControllerBase
    {
        private IResidencesService _residencesService;

        private static readonly Logger Logger = LogManager.GetLogger(typeof(ResidencesController).FullName);
        private readonly AppConfiguration _appConfiguration;

        public ResidencesController(IResidencesService residencesService,
                                  AppConfiguration appConfiguration)
        {
            _residencesService = residencesService;
            _appConfiguration = appConfiguration;
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPost(ApiRoutes.Residences.Create)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult Create([FromForm] CreateResidenceRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Invalid Request");

            var residence = _residencesService.Create(request.Name, request.Address, request.Description);

            var file = request.File;

            var uploadFilesPath = _appConfiguration.FileStorageBasePath;
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);

            string fileName;
            string fileNameThumb;
            if (file == null || file.Length == 0)
            {
                var defaultImage = Path.Combine(Directory.GetCurrentDirectory(),
                                                "Resources", "images", "residence.jpg");
                var defaultThumbnail = Path.Combine(Directory.GetCurrentDirectory(),
                                                    "Resources", "images", "residence_thumb.jpg");

                fileName = residence.Id + Path.GetExtension(defaultImage);
                fileNameThumb = residence.Id + "_thumb" + Path.GetExtension(defaultThumbnail);

                System.IO.File.Copy(defaultImage, Path.Combine(uploadFilesPath, fileName), true);
                System.IO.File.Copy(defaultThumbnail, Path.Combine(uploadFilesPath, fileNameThumb), true);
            }
            else
            {
                if (ImagesUtils.IsValid(file.Length) == false)
                    return BadRequest("Max file size exceeded.");
                if (ImagesUtils.IsValid(file.FileName) == false)
                    return BadRequest("Invalid file type.");

                UploadImages(residence, file, uploadFilesPath, out fileName, out fileNameThumb);
            }

            _residencesService.UpdateFileName(residence.Id, fileName, fileNameThumb);

            Logger.Info("Product created with code: " + request.Name + " -> id: " + residence.Id);

            return Ok(residence.Id);
        }

        [HttpGet(ApiRoutes.Residences.GetAll)]
        public IActionResult GetAll(GetResidencesFilter filter)
        {
            var users = _residencesService.GetAll(filter);
            var result = users.Select(x => new ResidenceModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Address = x.Address,
                ImageUrl = x.ImageUrl,
                ThumbUrl = x.ThumbnailUrl,
                IsAvailable = x.IsAvailable,
                CreatedAt = x.CreatedAt.GetValueOrDefault(new DateTime(2019, 01, 01)),
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            }).ToArray();

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Residences.Get)]
        public IActionResult GetById(int id)
        {
            var residence = _residencesService.Get(id);
            if (residence == null)
                return NotFound("Residence not found");

            var result = new ResidenceEditModel()
            {
                Id = residence.Id,
                Name = residence.Name,
                Address= residence.Address,
                Description = residence.Description,
                IsAvailable = residence.IsAvailable,
                ImageUrl = residence.ImageUrl,
                ThumbUrl = residence.ThumbnailUrl,
                CreatedAt = residence.CreatedAt.GetValueOrDefault(new DateTime(2019, 01, 01)),
                CreatedBy = residence.CreatedBy,
                UpdatedAt = residence.UpdatedAt,
                UpdatedBy = residence.UpdatedBy
            };

            return Ok(result);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpPut(ApiRoutes.Residences.Update)]
        public IActionResult Update(int id, [FromForm] EditResidenceRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Invalid Request");

            // map dto to entity and set id
            var residence = _residencesService.Get(id);
            if (residence == null)
                return NotFound("Product not found");


            _residencesService.Update(id, request.Name, request.Address, request.Description, request.IsAvailable);

            var file = request.File;

            var uploadFilesPath = _appConfiguration.FileStorageBasePath;
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);

            if (file != null && file.Length > 0)
            {
                if (ImagesUtils.IsValid(file.Length) == false)
                    return BadRequest("Max file size exceeded.");
                if (ImagesUtils.IsValid(file.FileName) == false)
                    return BadRequest("Invalid file type.");

                UploadImages(residence, file, uploadFilesPath, out var fileName, out var fileNameThumb);
                _residencesService.UpdateFileName(id, fileName, fileNameThumb);
            }

            Logger.Info("Residence updated with name: " + request.Name + " -> id: " + residence.Id);

            return Ok(residence.Id);
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete(ApiRoutes.Residences.Delete)]
        public IActionResult Delete(int id)
        {
            var residence = _residencesService.Get(id);
            if (residence == null)
                return NotFound();

            var imgPath = Path.Combine(_appConfiguration.FileStorageBasePath, residence.ImageUrl);
            var thumbPath = Path.Combine(_appConfiguration.FileStorageBasePath, residence.ThumbnailUrl);

            _residencesService.Delete(id);
            
            if (System.IO.File.Exists(imgPath))
                System.IO.File.Delete(imgPath);
            
            if (System.IO.File.Exists(thumbPath))
                System.IO.File.Delete(thumbPath);
            
            return Ok();
        }


        private void UploadImages(Residence residence, IFormFile file, string uploadFilesPath,
                                  out string fileName, out string fileNameThumb)
        {
            fileName = residence.Id + Path.GetExtension(file.FileName);
            fileNameThumb = residence.Id + "_thumb" + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFilesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
                Logger.Info("Image update and uploaded to: " + filePath);

                var thumb = ImagesUtils.GetReducedImage(_appConfiguration.ThumbnailMaxWidth,
                                                        _appConfiguration.ThumbnailMaxHeight, stream);
                thumb.Save(Path.Combine(uploadFilesPath, fileNameThumb));
                Logger.Info("Image thumbnail updated and uploaded to: " + Path.Combine(uploadFilesPath, fileNameThumb));
            }
        }
    }
}