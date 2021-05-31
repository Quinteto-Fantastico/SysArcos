﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysArcos;
namespace ProjetoArcos
{
    public partial class frmbuscaassistido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmassistido.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (ARCOS_Entities entities = new ARCOS_Entities())
            {

                IQueryable<ASSISTIDO> query;
                if (rdNome.Checked)
                {
                    query = entities.ASSISTIDO.Where(x => x.NOME.StartsWith(txtBusca.Text));
                }
                else
                {
                    query = entities.ASSISTIDO;
                }

                if (ddlTipoResponsabilidade.SelectedIndex == 1)
                {
                    query = query.Where(linha => linha.ASSISTIDO_TITULAR == null);
                }
                else if (ddlTipoResponsabilidade.SelectedIndex == 2)
                {
                    query = query.Where(linha => linha.ASSISTIDO_TITULAR != null);
                }
                var lista = query
                     .Select(linha => new
                         { 
                             linha.ID, ENTIDADE = linha.ENTIDADE.NOME, linha.NOME, linha.CPF, linha.DATA_NASCIMENTO, linha.PARENTESCO_ASSISTIDO_RESPONSAVEL,
                             RESPONSABILIDADE = linha.ASSISTIDO_TITULAR == null ? "TITULAR" : linha.ASSISTIDO_TITULAR.NOME
                         }
                     )
                     .OrderBy(linha=>linha.NOME)
                     .ToList();


                grid.DataSource = lista;
                grid.DataBind();
            }

        }

        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (grid.SelectedValue != null)
                Response.Redirect("frmassistido.aspx?ID=" + grid.SelectedValue.ToString());
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            if (grid.SelectedValue != null)
            {
                string ID = grid.SelectedValue.ToString();
                try
                {
                    using (ARCOS_Entities entities = new ARCOS_Entities())
                    {
                        ASSISTIDO assistido = entities.ASSISTIDO.FirstOrDefault(x => x.ID.ToString().Equals(ID));
                        if (assistido.ASSISTIDO_DEPENDENTES.Count > 0)
                        {
                            Response.Write("<script>alert('Este registro possui dependentes e não pode ser removido!');</script>");
                        }
                        else
                        {
                            entities.ASSISTIDO.Remove(assistido);
                            entities.SaveChanges();

                            grid.DataSource = null;
                            grid.DataBind();
                            grid.SelectedIndex = -1;
                            Response.Write("<script>alert('Removido com sucesso!');</script>");
                        }
                    }
                }
                catch
                {
                    Response.Write("<script>alert('Falha ao remover registro!');</script>");
                }
            }
        }
    }
}