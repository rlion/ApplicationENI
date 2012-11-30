using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using ApplicationENI.Modele;

namespace ApplicationENI.Vue.Rapports
{
    public partial class SyntheseECF : Form
    {
        private List<SessionECF> listeSessions;
        private List<Evaluation> listeEvaluations;
        private string nomStagiaire;

        public SyntheseECF(List<SessionECF> listeSessions, List<Evaluation> listeEvaluations, string nomStagiaire)
        {
            InitializeComponent();

            this.listeEvaluations = listeEvaluations;
            this.listeSessions = listeSessions;
            this.nomStagiaire = nomStagiaire;
        }

        private void SyntheseECF_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = "Vue\\Rapports\\ReportSyntheseECF.rdlc";
            
            ReportDataSource datasource = new ReportDataSource("DataSet1", listeSessions);
            
            //Initialisation du sous-rapport
            reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessHandler);
            reportViewer1.LocalReport.DataSources.Add(datasource);

            //Valorisation des paramètres
            IList<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("nomStagiaire", nomStagiaire));
            reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.RefreshReport();
        }

        private void SubreportProcessHandler(object sender, SubreportProcessingEventArgs e)
        {
          //DataSource du sous-rapport  
           e.DataSources.Add(new ReportDataSource("DataSet2", listeEvaluations));
         }

    }
}
