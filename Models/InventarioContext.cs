﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Prueba2_.Net_Proyecto.Models;

public partial class InventarioContext : DbContext
{
    public InventarioContext()
    {
    }

    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<productos> productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;database=tienda;user=root;password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<productos>(entity =>
        {
            entity.HasKey(e => e.idProducto).HasName("PRIMARY");

            entity.Property(e => e.creacion)
                .HasMaxLength(6)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(6)'");
            entity.Property(e => e.nombre).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}