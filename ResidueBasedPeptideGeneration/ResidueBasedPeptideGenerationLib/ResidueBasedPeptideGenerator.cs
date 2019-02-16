using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Qazi.Peptides.PeptideGenerator;

namespace Qazi.Peptides
{
    public class ResidueBasedPeptideGenerator
    {
        private static PeptideData peptideData;
        private static DataRow peptideRow;
        private static DataTable peptideDataTable;
        private static int _SizeOfOneSide;

        public static DataTable ExtractPeptide(string sequence, List<string> listOfTargetResidues, int sizeOfOneSide, bool isExtendedSequence)
        {
            List<int> listOfTargetPositions = new List<int>();
            int aminoIndex, totalAminoAcids;
            string aminoAcid;
            _SizeOfOneSide = sizeOfOneSide;

            peptideDataTable = new DataTable("PeptideDataTable");
            peptideDataTable.Columns.Add("ExtendedSequence");
            peptideDataTable.Columns.Add("Position");

            int index;
            for (index = (-1 * sizeOfOneSide); index <= sizeOfOneSide; index++)
                peptideDataTable.Columns.Add("P" + index.ToString());

            if (isExtendedSequence == true)
            {

                char[] sep = { ',' };
                string[] aminoAcidArray = sequence.Split(sep);
                totalAminoAcids = aminoAcidArray.Length;
                for (aminoIndex = 0; aminoIndex < totalAminoAcids; aminoIndex++)
                {
                    aminoAcid = aminoAcidArray[aminoIndex];
                    if (listOfTargetResidues.Contains(aminoAcid) == true)
                    {
                        peptideData = PeptideCutter.ToPeptideFromExtendedSequence(sequence, aminoIndex + 1, sizeOfOneSide);
                        UpdatePeptideDataTable(aminoIndex + 1);
                    }
                }
            }
            else
            {
                totalAminoAcids = sequence.Length;
                for (aminoIndex = 0; aminoIndex < totalAminoAcids; aminoIndex++)
                {
                    aminoAcid = sequence[aminoIndex].ToString();
                    if (listOfTargetResidues.Contains(aminoAcid) == true)
                    {
                        peptideData = PeptideCutter.ToPeptideFromStandardSequence(sequence, aminoIndex + 1, sizeOfOneSide);
                        UpdatePeptideDataTable(aminoIndex+1);
                    }
                }
            }
            return peptideDataTable;
        }

        private static void UpdatePeptideDataTable(int position)
        {
            peptideRow = ResidueBasedPeptideGenerator.peptideDataTable.NewRow();
            peptideRow["ExtendedSequence"] = peptideData.ExtendedPeptideSequence;
            peptideRow["Position"] = position;
            int ctr = 0;
            int index;
            for (index = (-1 * ResidueBasedPeptideGenerator._SizeOfOneSide); index <= ResidueBasedPeptideGenerator._SizeOfOneSide; index++)
            {
                peptideRow["P" + index.ToString()] = ResidueBasedPeptideGenerator.peptideData.PeptideSequence[ctr];
                ctr++;
            }
            ResidueBasedPeptideGenerator.peptideDataTable.Rows.Add(peptideRow);
        }
    }
}




