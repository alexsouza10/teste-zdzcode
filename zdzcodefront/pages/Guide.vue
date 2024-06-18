<template>
    <div class="guide-page">
      <v-container fluid>
        <v-row justify="center">
          <v-col cols="12" md="8">
            <v-card class="guide-card">
              <v-card-title class="guide-title">Guia de Início Rápido</v-card-title>
              <v-card-text class="guide-text">
                <p>
                  Este guia ajudará você a começar rapidamente com o projeto de manutenção de veículos.
                </p>
                <ol>
                  <li>Adicione novos veículos clicando em "Salvar Veículo" na página de veículos.</li>
                  <li>Registre a manutenção dos veículos clicando em "Registro de manutenção" na página de Registro de manutenção.</li>
                  <li>Crie os tipos de serviço clicando em "Salvar tipo de serviço" na página de tipo de serviço.</li>
                  <li>Explore essas e outras funcionalidades disponíveis no menu de navegação.</li>
                </ol>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </template>
  
  <script>
  export default {
    name: 'QuickStartGuide'
  };
  </script>
  
  <style lang="scss">
  .guide-page {
    background-color: #111111;
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
    height: 100vh;
    padding: 0;
  
    .guide-card {
      width: 100%;
      border-radius: 10px;
      box-shadow: 0px 4px 20px rgba(247, 247, 247, 0.1);
      background-color: #dadada;
  
      .guide-title {
        font-size: 26px;
        font-weight: bold;
        color: #050505;
        text-align: center;
        justify-content: center;
        margin-top: 20px;
      }
  
      .guide-text {
        color: #050505;
        font-size: 26px;
        text-align: left;
        line-height: 2.0;
      }
    }
  }
  </style>
  