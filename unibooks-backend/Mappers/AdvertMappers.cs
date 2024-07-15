using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Dtos.Advert;
using unibooks_backend.Dtos.AdvertPicture;
using unibooks_backend.Models;

namespace unibooks_backend.Mappers
{
    public static class AdvertMappers
    {
        public static AdvertDto ToAdvertDto(this Advert advertModel)
        {
            return new AdvertDto
            {
                Id = advertModel.Id,
                Price = advertModel.Price,
                Condition = advertModel.Condition,
                CreatedOn = advertModel.CreatedOn,
                BookId = advertModel.BookId,
                Advertimages = advertModel.BookImages.Select(a => a.ToPictureDto()).ToList(),
                ISBN = advertModel.Book.ISBN,
                CoverUrl = advertModel.Book.CoverUrl,
                Title = advertModel.Book.Title
               
            };
        }

        public static AdvertPictureDto ToPictureDto(this BookImage bookImage)
        {
            return new AdvertPictureDto
            {
                Url = bookImage.Url,
            };
        }

        public static Advert ToAdvertfromCreate(this CreateAdvertRequestDto advertDto)
        {
            return new Advert
            {
                Condition = advertDto.Condition,
                Price = advertDto.Price,
                CreatedOn = DateTime.Now,
                BookId = advertDto.BookId
            };
        }


    }
}