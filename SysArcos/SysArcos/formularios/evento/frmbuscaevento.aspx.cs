using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos;
using SysArcos.utils;

namespace SysArcos.formularios.evento
{
    public partial class frmbuscaevento : System.Web.UI.Page
    {
        private String COD_VIEW = "CNVT"; 
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmevento.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {

            using (ARCOS_Entities entities = new ARCOS_Entities())
            {
                List<EVENTO> lista = null;
                if (rbNome.Checked)
                {
                    lista = entities.EVENTO.Where(x => x.NOME.StartsWith(TextBox1.Text))
                        .OrderBy(x => x.NOME)
                        .ToList();
                }
                else if (rbDescricao.Checked)
                {
                    lista = entities.EVENTO.Where(x => x.NOME.StartsWith(TextBox1.Text))
                        .OrderBy(x => x.DESCRICAO)
                        .ToList();
                }
                else
                {
                    lista = entities.EVENTO
                        .OrderBy(x => x.NOME)
                        .ToList();
                }
                GridView1.DataSource = lista;
                GridView1.DataBind();
            }
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
                            EVENTO evento = entities.EVENTO.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                            entities.EVENTO.Remove(evento);
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

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }

}
    
