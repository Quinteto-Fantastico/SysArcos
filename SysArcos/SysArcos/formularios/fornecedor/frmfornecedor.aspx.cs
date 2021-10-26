using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos
{
    public partial class frmfornecedor : System.Web.UI.Page
    {
        private String COD_VIEW = "FORN";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmbuscafornecedor.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           
        }

        private void limpar()
        {
            throw new NotImplementedException();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {


                using (ARCOS_Entities entity = new ARCOS_Entities())
                {
                    if (!Permissoes.validar(lblAcao.Text.Equals("NOVO") ? Acoes.INCLUIR : Acoes.ALTERAR,
                                            Session["usuariologado"].ToString(),
                                            COD_VIEW,
                                            entity))
                    {
                        Response.Write("<script>alert('Permissão Negada');</script>");
                    }
                    else
                    {
                        FORNECEDOR fornecedor = null;

                        if (lblAcao.Text.Equals("NOVO"))
                        {
                            fornecedor = new FORNECEDOR();
                        }
                        else
                        {
                            fornecedor = entity.FORNECEDOR.FirstOrDefault(x => x.ID.ToString().Equals(txtNome.Text));
                        }

                        fornecedor.NOME = txtNome.Text;
                        fornecedor.CNPJ = txtCNPJ.Text;
                        fornecedor.LOGRADOURO = txtLogradouro.Text;
                        fornecedor.NUMERO = txtNumero.Text;
                        fornecedor.BAIRRO = txtBairro.Text;
                        fornecedor.CEP = txtCEP.Text;
                        fornecedor.CIDADE = txtCidade.Text;
                        fornecedor.ESTADO = ddlEstado.Text;
                        fornecedor.OBSERVACOES = txtObs.Text;
                        if (lblAcao.Text.Equals("NOVO"))
                        {
                            entity.FORNECEDOR.Add(fornecedor);
                        }
                        else
                        {
                            entity.Entry(fornecedor);
                        }
                        entity.SaveChanges();
                        limpar();

                        Response.Write("<script>alert('Assistência salvo com Sucesso!');</script>");
                    }
                }
            }
            catch
            {
                Response.Write("<script>alert('Registro não pode ser salvo!');</script>");
            }

        }
    }
}