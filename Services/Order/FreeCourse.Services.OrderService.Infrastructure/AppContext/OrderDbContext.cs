﻿using FreeCourse.Services.OrderService.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Infrastructure.AppContext
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
            modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().OwnsOne(x => x.Address).WithOwner();
        }



    }
}
