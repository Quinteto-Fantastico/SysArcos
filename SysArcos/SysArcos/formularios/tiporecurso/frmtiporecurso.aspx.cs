﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos;
using SysArcos.utils;

namespace ProjetoArcos
{
    public partial class frmtiporecurso : System.Web.UI.Page
    {
        private String COD_VIEW = "TRER";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (ARCOS_Entities entities = new ARCOS_Entities())
                {
                    String ID = Request.QueryString["ID"];
                    if ((ID != null) && (!ID.Equals("")))
                    {

                        TIPO_RECURSO u = entities.TIPO_RECURSO.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                        if (u != null)
                        {
                            lblID.Text = u.ID.ToString();
                            txtDescricao.Text = u.DESCRICAO;
                            lblAcao.Text = "Alterando";

                        }
                    }

                }
            }
        }

        protected void btn_cadastrarrecurso_Click(object sender, EventArgs e)
        {
            // Criar conexão com o banco
            if (txtDescricao.Text == "")
            {
                Response.Write("<script>alert('Há campos obrigatorios não preenchidos!');</script>");
            }
            else
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
                            String pagina = HttpContext.Current.Request.Url.AbsolutePath;

                            TIPO_RECURSO tiporecurso = null;
                            if (lblAcao.Text.Equals("NOVO"))
                                tiporecurso = new TIPO_RECURSO();
                            else
                                tiporecurso = entity.TIPO_RECURSO.FirstOrDefault(x => x.ID.ToString().Equals(lblID.Text));

                            tiporecurso.DESCRICAO = txtDescricao.Text;
                            tiporecurso.DATA_HORA_CRIACAO_REGISTRO = DateTime.Now;

                            // Insere o objeto

                            if (lblAcao.Text.Equals("NOVO"))
                                entity.TIPO_RECURSO.Add(tiporecurso);
                            else
                                entity.Entry(tiporecurso);

                            //Salva no disco rígido
                            entity.SaveChanges();

                            // Commit
                            Response.Write("<script>alert('Tipo de evento cadastrado com sucesso!');</script>");

                            txtDescricao.Text = string.Empty;
                        }
                    }
                }
                catch
                {
                    Response.Write("<script>alert('Registro não pode ser salvo!');</script>");
                }
            }


        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmbuscatiporecurso.aspx");
        }

        private void limpar()
        {
            lblAcao.Text = "NOVO";
            txtDescricao.Text = string.Empty;
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            limpar();
        }
    }

}

