using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using shoppingCart.Data;
using shoppingCart.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Repositories

{
    public class CartRepository : ICartRepository
    {
        private readonly cartContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(cartContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart> AddToCart(Guid Id)
        {
            if (_context.Product.Any())
            {
                var res = await _context.Product.Where(x => x.Id == Id).FirstOrDefaultAsync();
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);
                var userId=Guid.Parse(decodedToken.Payload["UserId"].ToString());


                var currentUser =await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                var product = await _context.Product.Where(x => x.Id == Id).FirstOrDefaultAsync();

                var userCart = await _context.Cart.Where(x => x.Id == currentUser.UserCart.Id).FirstOrDefaultAsync();

                if (userCart != null)
                {
                    userCart.ProductId = product.Id;
                    product.CartId = userCart.Id;
                    product.UserCart.ProductId = userCart.ProductId;
                    _context.SaveChanges();
                }
                return userCart;
            }
            return null;
        }

        public async Task<string> RemoveProductFromCart(Guid Id)
        {
            if (Id != null)
            {
                var productToBeRemoved=await _context.Cart.Where(x=>x.ProductId==Id).FirstOrDefaultAsync();
                var product =await _context.Product.Where(x => x.Id == Id).FirstOrDefaultAsync();
                productToBeRemoved.Products.Remove(product);
                productToBeRemoved.ProductId = Guid.Empty;
                
                _context.SaveChanges();
                return ($"Product With {Id} removed from {productToBeRemoved.UserCart}'s cart ");

            }
            return ("Please Provide valid product");

        }

        public async Task<Product> AddProduct(Product product)
        {
            if (product.Id != null || product.Id != Guid.Empty)
            {
               await _context.Product.AddAsync(product);
            }
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task<Cart> ViewUserCart()
        {
            
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = Guid.Parse(decodedToken.Payload["UserId"].ToString());

            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user.UserCart;

        }

        public async Task<User> AddUserToDb(User request)
        {
            if (request.Email != null)
            {
                var user = _context.Users.Where(x=>x.Id==request.Id).FirstOrDefault();
                if(user==null)
                {
                    User adduser = new User
                    {
                        Id=request.Id,
                        Email=request.Email,
                        UserName=request.UserName,
                    };
                    _context.Users.Add(adduser);
                    await _context.SaveChangesAsync();
                }
            }

            if (_context.Users.Any())
            {
                var userExists = _context.Users.Where(x => x.Id == request.Id).FirstOrDefault();
                if (userExists.UserCart == null)
                {
                    Cart userCart = new Cart
                    {
                        Id = Guid.NewGuid(),
                        UserCart = userExists.UserName,
                        UserId=userExists.Id
                    };
                    _context.Cart.Add(userCart);
                    _context.SaveChanges();
                }

            }
            return request;
        }

    }
}
