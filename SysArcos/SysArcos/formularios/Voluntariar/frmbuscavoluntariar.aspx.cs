using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos
{
    public partial class buscarvoluntariar : System.Web.UI.Page
    {
        private String COD_VIEW = "CSNT";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (ARCOS_Entities entities = new ARCOS_Entities())
            {
                List<VOLUNTARIAR> lista = null;
                if (txtBusca.Text == string.Empty)
                {
                    lista = entities.VOLUNTARIAR.ToList();
                }
                else if (rbVoluntariado.Checked)
                {
                    lista = entities.VOLUNTARIAR.Where(x => x.VOLUNTARIADO.DESCRICAO.StartsWith(txtBusca.Text)).ToList();
                }
                else if (rbVoluntario.Checked)
                {
                    lista = entities.VOLUNTARIAR.Where(x => x.VOLUNTARIO.NOME.StartsWith(txtBusca.Text)).ToList();
                }
                grid.DataSource = lista.OrderBy(x => x.DATA_INICIAL);
                grid.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmvoluntariar.aspx");
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                using (ARCOS_Entities entity = new ARCOS_Entities())
                {
                    if (!Permissoes.validar(Acoes.REMOVER,
                                                Session["usuariologado"].ToString(),
                                                COD_VIEW,
                                                entity))
                    {
                        Response.Write("<script>alert('Permissão negada!');</script>");
                    }
                    else
                    {
                        int IDSelecionado = Convert.ToInt32(grid.SelectedValue.ToString());

                        VOLUNTARIAR aluno = entity.VOLUNTARIAR.FirstOrDefault(
                           linha => linha.ID.ToString().Equals(IDSelecionado.ToString())
                           );

                        entity.VOLUNTARIAR.Remove(aluno);

                        entity.SaveChanges();

                        //atualizaGrid(conexao);
                    }
                }
            }
            catch
            {
                Response.Write("<script>alert('Registro não pode ser salvo!');</script>");
            }
        }



        private void atualizaGrid(ARCOS_Entities conexao)
        {
            var lista = conexao.VOLUNTARIAR.ToList();
            grid.DataSource = lista;
            grid.DataBind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedValue != null)
                //Redireciona para a página de cadastro com o login como parâmtro
                Response.Redirect("frmvoluntariar.aspx?id=" + grid.SelectedValue.ToString());
        }
    }
}