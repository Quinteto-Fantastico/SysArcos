using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos.formularios.estoque
{
    public partial class frmbuscaestoque : System.Web.UI.Page
    {
        private String COD_VIEW = "CLEQ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using(ARCOS_Entities conn = new ARCOS_Entities())
                {
                    carregarGrid(conn);
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmestoque.aspx");
        }

        private void carregarGrid(ARCOS_Entities conn)
        {
            List<ENTIDADE> lista = conn.ENTIDADE.
                Where(linha => linha.ATIVA == true).
                OrderBy(linha => linha.NOME).ToList();
            ddlEntidade.DataTextField = "NOME";//Carrega o campo que será mostrado
            ddlEntidade.DataValueField = "ID";//Carrega Primary Key
            ddlEntidade.DataSource = lista;
            ddlEntidade.DataBind();
            ddlEntidade.Items.Insert(0, "");
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedValue != null)
            {
                string ID = GridView1.SelectedValue.ToString();
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
                            ESTOQUE estoque = entities.ESTOQUE.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                            entities.ESTOQUE.Remove(estoque);
                            entities.SaveChanges();

                            //Limpar Grid 
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            GridView1.SelectedIndex = -1;
                            Response.Write("<script>alert('Removido com sucesso!');</script>");
                        }
                    }
                    catch
                    {
                        Response.Write("<script>alert('O evento não pode ser removido!');</script>");
                    }
                }
            }
        }
    }
}