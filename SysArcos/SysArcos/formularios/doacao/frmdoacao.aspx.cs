using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos.utils;

namespace SysArcos.formularios.doacao
{
    public partial class frmdoacao : System.Web.UI.Page
    {
        private String COD_VIEW = "DACO";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (ARCOS_Entities entities = new ARCOS_Entities())
                {
                    carregaDoador(entities);
                    carregaEntidade(entities);
                    String ID = Request.QueryString["ID"];
                    if ((ID != null) && (!ID.Equals("")))
                    {
                        DOACAO u = entities.DOACAO.FirstOrDefault(x => x.ID.Equals(ID));
                        if (u != null)
                        {
                            string ID_CONVERT = ID.ToString();
                        }
                    }
                }
            }
        }


        protected void btnBuscar_Click(object sender, EventArgs e) //Buscar
        {
            Response.Redirect("frmbuscadoacao.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e) //Salvar
        {
            if (txtData.Text == "" || txtDoacao.Text == "" || txtObs.Text == "")
            {
                Response.Write("Há campos obrigatórios não preenchidos");
            }
            else
            {
                 /*try //só treino(apagar)
                 {
                     using (ARCOS_Entities entities = new ARCOS_Entities())
                     {
                        if (!Permissoes.validar(lblAcao.Text.Equals("NOVO") ? Acoes.INCLUIR : Acoes.ALTERAR,
                                               Session["usuariologado"].ToString(),
                                               COD_VIEW,
                                               entities))
                        {
                            Response.Write("<script>alert('Permissão Negada');</script>");
                        }
                        else
                        {
                            DOACAO doacao = null;

                            if (lblAcao.Text.Equals("NOVO"))
                            {
                                doacao = new DOACAO();
                            }
                            else
                            {
                                doacao = entities.DOACAO.FirstOrDefault(linha => linha.ID.ToString().Equals(txtDoacao.Text));
                            }

                            doacao.DESCRICAO = txtDoacao.Text;
                            doacao.DATA = DateTime.ParseExact(txtData.Text, "dd/MM/yyyy", null);
                            doacao.DOADOR = entities.DOADOR.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlDoador.SelectedValue));
                            doacao.ENTIDADE = entities.ENTIDADE.FirstOrDefault(linha => linha.ID.ToString().Equals(ddlEntidade.SelectedValue));
                            doacao.DATA_HORA_CRIACAO_REGISTRO = DateTime.Now;
                            doacao.OBSERVACOES = txtObs.Text;

                            if (lblAcao.Text.Equals("NOVO"))
                            {
                                entities.DOACAO.Add(doacao);
                            }
                            else
                            {
                                entities.Entry(doacao);
                            }
                            entities.SaveChanges();
                            limpar();
                        }
                     }
                 }
                 catch
                 {
                     Response.Write("Registro não pode ser salvo!");
                 }*/
            }
        }

        private void limpar()
        {
            if (ddlEntidade.Items.Count == 2)
            {
                ddlEntidade.SelectedIndex = 1;
            }
            else
            {
                ddlEntidade.SelectedIndex = 0;
            }
            txtData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDoacao.Text = string.Empty;
            txtObs.Text = string.Empty;
            ddlDoador.SelectedIndex = 0;
            //lblAcao.Text = "NOVO";
        }

        private void carregaEntidade(ARCOS_Entities conn)
        {
            List<ENTIDADE> list = conn.ENTIDADE.OrderBy(x => x.NOME).ToList();
            ddlEntidade.DataTextField = "NOME";//Carrega o campo que será mostrado
            ddlEntidade.DataValueField = "ID";//Carrega Primary Key
            ddlEntidade.DataSource = list;
            ddlEntidade.DataBind();
            ddlEntidade.Items.Insert(0, "");
        }

        private void carregaDoador(ARCOS_Entities conn)
        {
            List<DOADOR> list = conn.DOADOR.OrderBy(x => x.NOME).ToList();
            ddlDoador.DataTextField = "NOME";//Carrega o campo que será mostrado
            ddlDoador.DataValueField = "ID";//Carrega Primary Key
            ddlDoador.DataSource = list;
            ddlDoador.DataBind();
            ddlDoador.Items.Insert(0, "");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            limpar();
        }
    }
}