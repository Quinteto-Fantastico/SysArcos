using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysArcos.formularios.recurso
{
    public partial class recurso : System.Web.UI.Page
    {
        private String COD_VIEW = "RECR";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (ARCOS_Entities conn = new ARCOS_Entities())
                {

                    carregarEntidade(conn);
                    carregarTipoRecurdo(conn);
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmbuscarecurso.aspx");
        }

        private void carregarEntidade(ARCOS_Entities conn)
        {
            List<ENTIDADE> lista =
                conn.ENTIDADE.Where(linha => linha.ATIVA == true).
                OrderBy(linha => linha.NOME).ToList();
            ddlEntidade.DataTextField = "NOME";
            ddlEntidade.DataValueField = "ID";
            ddlEntidade.DataSource = lista;
            ddlEntidade.DataBind();
            ddlEntidade.Items.Insert(0, "");
        }

        private void carregarTipoRecurdo(ARCOS_Entities conn)
        {
            List<TIPO_RECURSO> lista =
                conn.TIPO_RECURSO.
                OrderBy(linha => linha.DESCRICAO).ToList();
            ddlTipoRecurso.DataTextField = "DESCRICAO";
            ddlTipoRecurso.DataValueField = "ID";
            ddlTipoRecurso.DataSource = lista;
            ddlTipoRecurso.DataBind();
            ddlTipoRecurso.Items.Insert(0, "");
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
    }
}