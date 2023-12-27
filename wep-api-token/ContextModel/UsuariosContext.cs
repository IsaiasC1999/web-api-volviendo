using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wep_api_token.ContextModel;

public partial class UsuariosContext : DbContext
{
    

    public UsuariosContext(DbContextOptions<UsuariosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<DetalleBlog> DetalleBlogs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("blogs_pkey");

            entity.ToTable("blogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Autor)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("autor");
            entity.Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");
            entity.Property(e => e.SubTitulo)
                .IsRequired()
                .HasColumnName("sub_titulo");
            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<DetalleBlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("detalle_blog_pkey");

            entity.ToTable("detalle_blog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.ImagenDos)
                .HasMaxLength(255)
                .HasColumnName("imagen_dos");
            entity.Property(e => e.ImagenUno)
                .HasMaxLength(255)
                .HasColumnName("imagen_uno");
            entity.Property(e => e.ParrafoDos).HasColumnName("parrafo_dos");
            entity.Property(e => e.ParrafoUno).HasColumnName("parrafo_uno");
            entity.Property(e => e.Subtitulo).HasColumnName("subtitulo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Blog).WithMany(p => p.DetalleBlogs)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("detalle_blog_blog_id_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContrasenaHash)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.TokenAcceso)
                .HasMaxLength(255)
                .HasColumnName("token_acceso");
            entity.Property(e => e.TokenActualizacion)
                .HasMaxLength(255)
                .HasColumnName("token_actualizacion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
