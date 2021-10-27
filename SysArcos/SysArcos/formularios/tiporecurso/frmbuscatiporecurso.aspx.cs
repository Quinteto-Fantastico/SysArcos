using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos;
using SysArcos.utils;

namespace ProjetoArcos
{
    public partial class frmbuscatiporecurso : System.Web.UI.Page
    {
        private String COD_VIEW = "CRLT";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (ARCOS_Entities entities = new ARCOS_Entities())
                {
                    String pagina = HttpContext.Current.Request.Url.AbsolutePath;
                    validaPermissao(pagina);

                }
            }

        }
        private void validaPermissao(String pagina)
        {
            using (ARCOS_Entities entity = new ARCOS_Entities())
            {
                string login = (string)Session["usuariologado"];
                USUARIO u =
                    entity.USUARIO.FirstOrDefault(linha => linha.LOGIN.Equals(login));
                if (!u.ADM)
                {
                    SISTEMA_ENTIDADE item = entity.SISTEMA_ENTIDADE.FirstOrDefault(x => x.URL.Equals(pagina));
                    if (item != null)
                    {
                        SISTEMA_ITEM_ENTIDADE perm = u.GRUPO_PERMISSAO.SISTEMA_ITEM_ENTIDADE.FirstOrDefault(x => x.ID_SISTEMA_ENTIDADE.ToString().Equals(item.ID.ToString()));
                        if (perm == null)
                        {
                            Response.Redirect("/permissao_negada.aspx");
                        }
                    }
                }
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            using (ARCOS_Entities entities = new ARCOS_Entities())
            {
                List<TIPO_RECURSO> lista = null;
                if (rddescricao.Checked)
                {
                    lista = entities.TIPO_RECURSO.Where(x => x.DESCRICAO.StartsWith(txtbusca.Text)).ToList();


                }
                else if (rddescricao.Checked)
                {
                    lista = entities.TIPO_RECURSO.Where(x => x.DESCRICAO.StartsWith(txtbusca.Text)).ToList();

                }
                else
                {
                    lista = entities.TIPO_RECURSO.ToList();

                }
                Grid.DataSource = lista.OrderBy(x => x.DESCRICAO);

                Grid.DataBind();
            }
        }

        protected void btnremover_Click(object sender, EventArgs e)
        {
            if (Grid.SelectedValue != null)
            {
                string ID = Grid.SelectedValue.ToString();
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
                            TIPO_RECURSO TIPO_RECURSO = entities.TIPO_RECURSO.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                            entities.TIPO_RECURSO.Remove(TIPO_RECURSO);
                            entities.SaveChanges();

                            Grid.DataSource = null;
                            Grid.DataBind();
                            Grid.SelectedIndex = -1;
                            Response.Write("<script>alert('Removido com sucesso!');</script>");
                        }
                    }
                    catch
                    {
                        Response.Write("<script>alert('Falha ao remover registro!');</script>");
                    }
                }

            }
        }

        protected void btnselecionar_Click(object sender, EventArgs e)
        {
            if (Grid.SelectedValue != null)
                //Redireciona para a página de cadastro com o login como parâmtro
                Response.Redirect("frmtiporecurso.aspx?ID=" + Grid.SelectedValue.ToString());
        }

        protected void btnvoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmtiporecurso.aspx");
        }
    }
}
