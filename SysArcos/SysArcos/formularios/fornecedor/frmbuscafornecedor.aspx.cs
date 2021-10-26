using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;


namespace SysArcos
{
    public partial class frmbuscafornecedor : System.Web.UI.Page
    {
        private String COD_VIEW = "CODO";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmfornecedor.aspx");
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            if (grid.SelectedValue != null)
            {
                string ID = grid.SelectedValue.ToString();
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
                            FORNECEDOR fornecedor = entities.FORNECEDOR.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                            entities.FORNECEDOR.Remove(fornecedor);
                            entities.SaveChanges();

                            //Limpar Grid 
                            grid.DataSource = null;
                            grid.DataBind();
                            grid.SelectedIndex = -1;
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