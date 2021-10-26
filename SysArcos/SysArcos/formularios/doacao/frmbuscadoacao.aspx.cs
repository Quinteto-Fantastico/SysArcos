using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos.formularios.doacao
{
    public partial class frmbuscadoacao : System.Web.UI.Page
    {
        private String COD_VIEW = "CUDR";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmdoacao.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            if (gridBusca.SelectedValue != null)
            {
                string ID = gridBusca.SelectedValue.ToString();
                using (ARCOS_Entities entities = new ARCOS_Entities())
                {
                    try
                    {
                        if (!Permissoes.validar(Acoes.REMOVER,
                                            Session["usuariologado"].ToString(),
                                            COD_VIEW,
                                            entities))
                        {
                            Response.Write("<script>alert('Permissão negada!');</script>");
                        }
                        else
                        {
                            //Remove linha e salva
                            DOACAO doacoes = entities.DOACAO.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                            entities.DOACAO.Remove(doacoes);
                            entities.SaveChanges();

                            //Limpar grid 
                            gridBusca.DataSource = null;
                            gridBusca.DataBind();
                            gridBusca.SelectedIndex = -1;
                            Response.Write("<script>alert('Removido com sucesso!');</script>");
                        }
                    }
                    catch
                    {
                        Response.Write("<script>alert('Registro não pode ser removido!');</script>");
                    }
                }
            }
        }

    }
}