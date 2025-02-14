using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        //** ESSA É A HORA QUE VC BRILHA **//
        public DbSet<Professores> Professores { get; set; }

        public DbSet<Alunos> Alunos { get; set; }

        public DbSet<Materias> Materias { get; set; }

        public DbSet<Etapas> Etapas { get; set; }

        public DbSet<Notas> Notas { get; set; }

        //** ESSA É A HORA DE PARAR **//

    }
}

