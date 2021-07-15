using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClosedXML.Excel;
using PlanilhaCEP.API_Correios;
using System.Diagnostics;

namespace PlanilhaCEP
{
    class PlanilhaDeEntrada
    {
        Invoker invoker = new Invoker();
        public PlanilhaDeEntrada()
        {
            var xlsDocumento = new XLWorkbook(@"C:\pathFile.xlsx");
            var paginaPlanilha = xlsDocumento.Worksheets.First();

            var totalLinhas = paginaPlanilha.Rows().Count();

            // Cinco primeiras linhas são o cabeçalho
            for (int linha = 6; linha <= totalLinhas; linha++)
            {
                var coluna = "J";
                var nomeCidade = "";
                var cep = paginaPlanilha.Cell($"{coluna}{linha}").Value.ToString();

                if (string.IsNullOrWhiteSpace(cep))
                    linha++;
                else
                    nomeCidade = invoker.NomeDaCidade(cep);
                    Console.WriteLine($"Número da linha: {linha}");
                    InserirCidades(paginaPlanilha, xlsDocumento, linha.ToString(), nomeCidade);
            }
        }
        
        //Função para inserir cidade em alguma coluna/linha da planilha
        void InserirCidades(IXLWorksheet worksheet, XLWorkbook xls, string linha, string nomeCidade)
        {
            let coluna = "F";
            worksheet.Cell($"{coluna}{linha}").Value = $"{nomeCidade}";
            xls.SaveAs("planilhaOutput.xlsx");
        }
    }
}
