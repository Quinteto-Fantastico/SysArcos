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
    public partial class frmevento : System.Web.UI.Page
    {
        private String COD_VIEW = "EVET";
        protected void Page_Load(object sender, EventArgs e)
        {
            string Data_Atual = DateTime.Today.ToString();
            if (!IsPostBack)
            {
                if (txtDataInicio.Text != Data_Atual)
                {
                    btnSalvar.Enabled = true;
                }
                else
                {
                    btnSalvar.Enabled = false;
                }
                carregarEntidade();
                carregaTipoEvento();
            }
        }

        private void carregaTipoEvento()
        {
            using (ARCOS_Entities enntity = new ARCOS_Entities())
            {
                List<TIPO_EVENTO> lista = enntity.TIPO_EVENTO.ToList();
                ddlTipoEvento.DataTextField = "NOME";
                ddlTipoEvento.DataValueField = "ID";
                ddlTipoEvento.DataSource = lista;
                ddlTipoEvento.DataBind();
                ddlTipoEvento.Items.Insert(0, "");
            }
        }

        private void carregarEntidade()
        {
            using (ARCOS_Entities enntity = new ARCOS_Entities())
            {
                List<ENTIDADE> lista = enntity.ENTIDADE.
                    Where(linha => linha.ATIVA.Equals(true)).
                    OrderBy(linha => linha.NOME).ToList();
                ddlEntidade.DataTextField = "NOME";
                ddlEntidade.DataValueField = "ID";
                ddlEntidade.DataSource = lista;
                ddlEntidade.DataBind();
                ddlEntidade.Items.Insert(0, "");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmbuscaevento.aspx");

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

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
                        EVENTO evento = null;

                        if (lblAcao.Text.Equals("NOVO"))
                        {
                            evento = new EVENTO();
                        }
                        else
                        {
                            evento = entity.EVENTO.FirstOrDefault(x => x.ID.ToString().Equals(txtNome.Text));
                        }

                        evento.ENTIDADE = entity.ENTIDADE.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlEntidade.SelectedValue));
                        evento.TIPO_EVENTO = entity.TIPO_EVENTO.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlTipoEvento.SelectedValue));
                        evento.NOME = txtNome.Text;
                        evento.DESCRICAO = txtDescricao.Text;
                        evento.DATA_HORA_INICIO = DateTime.ParseExact(txtDataInicio.Text, "dd/MM/yyyy", null);
                        evento.DATA_HORA_TERMINO = DateTime.ParseExact(txtHoraFim.Text, "dd/MM/yyyy", null);


                        if (lblAcao.Text.Equals("NOVO"))
                        {
                            entity.EVENTO.Add(evento);
                        }
                        else
                        {
                            entity.Entry(evento);
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
            throw new NotImplementedException();
        }
    }      
 }           