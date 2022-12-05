using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStore.Services.Images
{
    public interface IImageStorage
    {
        Task<string> SaveImageAsync(string name, Stream stream);
        Task RemoveImageAsync(string name);
    }
}
