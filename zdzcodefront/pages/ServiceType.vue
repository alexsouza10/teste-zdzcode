<template>
  <v-card>
      <!-- Formulário para adicionar/editar tipos de serviço -->
      <v-container fluid>
          <v-form ref="form" v-model="valid">
              <v-text-field
                  v-model="serviceType.name"
                  :rules="[v => !!v || 'Nome é obrigatório']"
                  label="Nome"
                  required
              ></v-text-field>
              <v-text-field
                  v-model="serviceType.description"
                  label="Descrição"
              ></v-text-field>
              <v-text-field v-if="editMode" v-model="serviceType.id" label="ID" required></v-text-field>

              <v-btn :disabled="!valid" color="primary" class="mr-4" @click="submit">{{ editMode ? 'Salvar Alteração' : 'Salvar' }}</v-btn>
              <v-btn color="error" @click="resetForm">Resetar</v-btn>
              <v-btn color="secondary" @click="fetchServiceTypes">Listar Tipos de Serviço</v-btn>
              <v-btn color="warning" @click="toggleEditMode">{{ editMode ? 'Cancelar Alteração' : 'Alterar Tipo de Serviço' }}</v-btn>
              <!-- Botão para habilitar alteração de tipo de serviço -->
          </v-form>
      </v-container>

      <!-- Tabela de tipos de serviço -->
      <v-data-table v-if="serviceTypes.length > 0" :headers="headers" :items="serviceTypes" :items-per-page="5"
          class="elevation-1">
          <template v-slot:item.actions="{ item }">
              <v-icon small @click="deleteServiceType(item)">mdi-delete</v-icon>
          </template>
          <template v-slot:item.id="{ item }">
              {{ item.id }}
          </template>
          <template v-slot:no-data>
              <v-alert :value="true" color="error" icon="mdi-alert">
                  Nenhum tipo de serviço encontrado.
              </v-alert>
          </template>
      </v-data-table>
      <div v-else>
          <v-alert :value="true" color="info" icon="mdi-information">
              Carregando tipos de serviço...
          </v-alert>
      </div>
  </v-card>
</template>

<script>
import axios from 'axios';
import https from 'https';

export default {
  data() {
      return {
          valid: true, // Variável para armazenar a validade do formulário
          serviceType: {
              id: null,
              name: "",
              description: "",
          },
          headers: [
              { text: "ID", value: "id" },
              { text: "Nome", value: "name" },
              { text: "Descrição", value: "description" },
              { text: 'Actions', value: 'actions', sortable: false },
          ],
          serviceTypes: [], // Array para armazenar os tipos de serviço da API
          editMode: false, // Modo de edição
      };
  },

  methods: {
      async submit() {
          if (!this.$refs.form.validate()) {
              return;
          }

          try {
              let payload = { ...this.serviceType };
              if (payload.id === null) {
                  delete payload.id;
              } else {
                  payload.id = parseInt(payload.id);
              }

              const method = payload.id ? 'put' : 'post';
              const url = `https://localhost:7171/api/ServiceTypes${payload.id ? `/${payload.id}` : ''}`;

              const response = await axios({
                  method,
                  url,
                  data: payload,
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });

              if (response.status === 201 || response.status === 200 || response.status === 204) {
                  alert("Dados do tipo de serviço salvos com sucesso!");
                  await this.fetchServiceTypes();
                  this.resetForm();
              } else {
                  alert("Erro ao salvar os dados do tipo de serviço. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao enviar dados para o backend:", error);
              alert("Erro ao salvar os dados do tipo de serviço. Por favor, tente novamente.");
          }
      },

      async fetchServiceTypes() {
          try {
              const response = await axios.get('https://localhost:7171/api/ServiceTypes', {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 200) {
                  this.serviceTypes = response.data.$values;
              } else {
                  alert("Erro ao buscar os tipos de serviço. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao buscar tipos de serviço:", error);
              alert("Erro ao buscar os tipos de serviço. Por favor, tente novamente.");
          }
      },

      resetForm() {
          this.serviceType = {
              id: null,
              name: "",
              description: "",
          };
          this.editMode = false;
          this.$refs.form.resetValidation();
      },

      setEditServiceType(serviceType) {
          this.serviceType = { ...serviceType };
          this.editMode = true;
      },

      async deleteServiceType(serviceType) {
          try {
              const response = await axios.delete(`https://localhost:7171/api/ServiceTypes/${serviceType.id}`, {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 204 || response.status === 200) {
                  alert("Tipo de serviço excluído com sucesso!");
                  await this.fetchServiceTypes();
              } else {
                  alert("Erro ao excluir o tipo de serviço. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao excluir o tipo de serviço:", error);
              alert("Erro ao excluir o tipo de serviço. Por favor, tente novamente.");
          }
      },

      toggleEditMode() {
          if (this.editMode) {
              this.resetForm(); // Se estiver no modo de edição e clicar no botão novamente, cancelar a edição
          } else {
              this.editMode = true; // Caso contrário, apenas ativa o modo de edição
          }
      },
  },

  created() {
      this.fetchServiceTypes();
  },
};
</script>
