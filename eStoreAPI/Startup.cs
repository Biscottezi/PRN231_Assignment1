using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace eStoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eStoreAPI", Version = "v1" });
            });
            var config = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Member, MemberViewModel>().ReverseMap();
                mc.CreateMap<MemberCreateModel, Member>().ReverseMap();
                mc.CreateMap<Product, ProductViewModel>()
                    .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                    .ReverseMap();
                mc.CreateMap<ProductCreateModel, Product>().ReverseMap();
                mc.CreateMap<Order, OrderViewModel>().ReverseMap();
                mc.CreateMap<OrderCreateModel, Order>().ReverseMap();
                mc.CreateMap<OrderDetail, OrderDetailViewModel>()
                    .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                    .ReverseMap();
                mc.CreateMap<OrderDetailCreateModel, OrderDetail>().ReverseMap();
                mc.CreateMap<Category, CategoryViewModel>().ReverseMap();
                mc.CreateMap<ProductViewModel, ProductCreateModel>().ReverseMap();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eStoreAPI v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}