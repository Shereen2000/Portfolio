using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unibooks_backend.Dtos.Book;
using unibooks_backend.Models;

namespace unibooks_backend.Mappers
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookmodel)
        {
            return new BookDto{
                Id = bookmodel.Id,
                ISBN = bookmodel.ISBN,
                Title = bookmodel.Title,
                Publisher = bookmodel.Publisher,
            };
        }

        public static Book ToBookfromCreateDto(this CreateBookRequestDto bookdto)
        {
            return new Book{
                ISBN = bookdto.ISBN,
                Title = bookdto.Title,
                Publisher = bookdto.Publisher,
            };
        }
    }
}