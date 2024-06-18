<template>
  <div class="welcome-page">
    <v-container fluid>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card class="welcome-card">
            <v-card-title class="welcome-title">Bem-vindo ao Projeto de Manutenção de Veículos</v-card-title>
            <v-card-text class="welcome-text">
              <p>
                Este é um projeto para gerenciar o registro de manutenção de veículos. Aqui você pode adicionar, visualizar, editar e excluir informações sobre veículos, serviços de manutenção e tipos de serviço.
              </p>
              <p>
                Use os formulários fornecidos para adicionar novos veículos, registros de manutenção e tipos de serviço.
              </p>
            </v-card-text>
            <v-card-actions class="welcome-actions">
              <router-link to="/guide" class="guide-button" color="secondary" outlined>Guia de Início Rápido</router-link>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: 'WelcomePage'
};
</script>

<style lang="scss" scoped>
.welcome-page {
  background-color: #131313;
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;

  .welcome-card {
    width: 100%;
    border-radius: 10px;
    box-shadow: 0px 4px 20px rgba(0, 0, 0, 0.1);
    background-color: #dadada;

    .welcome-title {
      font-size: 26px;
      font-weight: bold;
      color: #000000;
      text-align: center;
      justify-content: center;
      margin-top: 20px;
    }

    .welcome-text {
      color: #000000;
      font-size: 22px;
      text-align: center;
      line-height: 1.6;
    }

    .welcome-actions {
      display: flex;
      justify-content: center;

      .guide-button {
        background-color: #4686be;
        color: #fff;
        font-size: 14px;
        font-weight: bold;
        text-transform: uppercase;
        margin: 10px;
        padding: 10px;
        border-radius: 20px;
      }
    }
  }
}
</style>
