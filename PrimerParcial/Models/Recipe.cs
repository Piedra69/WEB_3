using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using PrimerParcial.Models;

namespace PrimerParcial.Models
{
    public class Recipes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public int Servings { get; set; }
        public string Instructions { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        = DateTime.UtcNow;
        public int CategoryId { get; set; }
        public Category Category { get; set; }



    }
}
//Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
  //                  PreparationTimeMinutes = table.Column<int>(type: "int", nullable: false),
  //                  Servings = table.Column<int>(type: "int", nullable: false),
    //                Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
      //              DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            CategoryId