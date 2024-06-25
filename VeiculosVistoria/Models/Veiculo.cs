using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeiculosVistoria.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public String? Placa { get; set; }
        public String? Chassi { get; set; }
        public String? Motor { get; set; }
        public Int32 Ano_Fabricacao { get; set; }
        public Int32 Ano_Modelo { get; set; }
        public String? Marca { get; set; }
        public String? Linha { get; set; }
        public String? Descricao { get; set; }
        public Double? Potencia { get; set; }
        public String? Observacoes { get; set; }
    }
}

//use Chassi

//drop table Veiculos

//CREATE TABLE Veiculos(
//	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
//    Placa varchar(7) NULL,
//    Chassi varchar(100) NOT NULL,
//    Motor varchar(100) NULL,
//    Ano_Fabricacao int NOT NULL,
//    Ano_Modelo int NOT NULL,
//    Marca varchar(100) NULL,
//    Linha varchar(100) NULL,
//    Descricao varchar(100) NULL,
//    Potencia decimal(6, 2) NULL,
//    Observacoes varchar(100) NULL
//)

//CREATE NONCLUSTERED INDEX IX_Veiculos_Placa
//ON Veiculos(Placa)

//CREATE NONCLUSTERED INDEX IX_Veiculos_Chassi
//ON Veiculos(Chassi)
