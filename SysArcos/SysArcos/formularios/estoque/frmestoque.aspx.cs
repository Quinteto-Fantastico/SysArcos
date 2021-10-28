using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos.formularios.estoque
{
    public partial class frmestoque : System.Web.UI.Page
    {
        private String COD_VIEW = "ESTQ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using(ARCOS_Entities conn = new ARCOS_Entities())
                {
                    carregaEntidade(conn);
                    carregaProduto(conn);
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmbuscaestoque.aspx");
        }

        private void carregaProduto(ARCOS_Entities conn)
        {
            List<PRODUTO> list = conn.PRODUTO.OrderBy(x => x.DESCRICAO).ToList();
            ddlProduto.DataTextField = "DESCRICAO";//Carrega o campo que será mostrado
            ddlProduto.DataValueField = "ID";//Carrega Primary Key
            ddlProduto.DataSource = list;
            ddlProduto.DataBind();
            ddlProduto.Items.Insert(0, "");
        }

        private void carregaEntidade( ARCOS_Entities conn)
        {
            List<ENTIDADE> list = conn.ENTIDADE.OrderBy(x => x.NOME).ToList();
            ddlEntidade.DataTextField = "NOME";//Carrega o campo que será mostrado
            ddlEntidade.DataValueField = "ID";//Carrega Primary Key
            ddlEntidade.DataSource = list;
            ddlEntidade.DataBind();
            ddlEntidade.Items.Insert(0, "");
        }

        protected void Button1_Click(object sender, EventArgs e)
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

                                ESTOQUE estoque = null;

                                if (lblAcao.Text.Equals("NOVO"))
                                {
                                    estoque = new ESTOQUE();
                                }
                                else
                                {
                                    estoque = entity.ESTOQUE.FirstOrDefault(x => x.ID.ToString().Equals(ddlEntidade.Text));
                                }
                                estoque.ENTIDADE = entity.ENTIDADE.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlEntidade.SelectedValue));
                                estoque.PRODUTO = entity.PRODUTO.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlProduto.SelectedValue));
                                estoque.QUANTIDADE = Convert.ToInt32(txtQuantidade.Text);
                                if (lblAcao.Text.Equals("NOVO"))
                                {
                                    entity.ESTOQUE.Add(estoque);
                                }
                                else
                                {
                                    entity.Entry(estoque);
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

        private void limpar()
        {
            ddlEntidade.SelectedValue = "";
            ddlProduto.SelectedValue = "";
            txtQuantidade.Text = "";
        }
    }

}