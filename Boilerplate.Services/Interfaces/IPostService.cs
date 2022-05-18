using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Services.Interfaces
{
    public interface IPostService
    {
        List<Post> GetPostsByBlogId(int blogId);
    }
}
