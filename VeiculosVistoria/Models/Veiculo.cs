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
        public string? Placa { get; set; }
        public string? Chassi { get; set; }
        public string? Motor { get; set; }
        public int Ano_Fabricacao { get; set; }
        public int Ano_Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Linha { get; set; }
        public string? Descricao { get; set; }
        public double? Potencia { get; set; }
        public string? Observacoes { get; set; }
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
