<template>
  <v-card>
      <!-- Formulário para adicionar/editar serviços de manutenção -->
      <v-container fluid>
          <v-form ref="form" v-model="valid">
              <v-text-field
                  v-model="service.name"
                  :rules="[v => !!v || 'Nome é obrigatório']"
                  label="Nome"
                  required
              ></v-text-field>
              <v-text-field
                  v-model="service.description"
                  :rules="[v => !!v || 'Descrição é obrigatória']"
                  label="Descrição"
                  required
              ></v-text-field>
              <v-text-field v-if="editMode" v-model="service.id" label="ID" required></v-text-field>

              <v-btn :disabled="!valid" color="primary" class="mr-4" @click="submit">{{ editMode ? 'Salvar Alteração' : 'Salvar' }}</v-btn>
              <v-btn color="error" @click="resetForm">Resetar</v-btn>
              <v-btn color="secondary" @click="fetchServices">Listar Serviços</v-btn>
              <v-btn color="warning" @click="toggleEditMode">{{ editMode ? 'Cancelar Alteração' : 'Alterar Serviço' }}</v-btn>
              <!-- Botão para habilitar alteração de serviço -->
          </v-form>
      </v-container>

      <!-- Tabela de serviços de manutenção -->
      <v-data-table v-if="services.length > 0" :headers="headers" :items="services" :items-per-page="5"
          class="elevation-1">
          <template v-slot:item.actions="{ item }">
              <v-icon small @click="deleteService(item)">mdi-delete</v-icon>
          </template>
          <template v-slot:item.id="{ item }">
              {{ item.id }}
          </template>
          <template v-slot:no-data>
              <v-alert :value="true" color="error" icon="mdi-alert">
                  Nenhum serviço encontrado.
              </v-alert>
          </template>
      </v-data-table>
      <div v-else>
          <v-alert :value="true" color="info" icon="mdi-information">
              Carregando serviços...
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
          service: {
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
          services: [], // Array para armazenar os serviços de manutenção da API
          editMode: false, // Modo de edição
      };
  },

  methods: {
      async submit() {
          if (!this.$refs.form.validate()) {
              return;
          }

          try {
              let payload = { ...this.service };
              if (payload.id === null) {
                  delete payload.id;
              } else {
                  payload.id = parseInt(payload.id);
              }

              const method = payload.id ? 'put' : 'post';
              const url = `https://localhost:7171/api/MaintenanceServices${payload.id ? `/${payload.id}` : ''}`;

              const response = await axios({
                  method,
                  url,
                  data: payload,
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });

              if (response.status === 201 || response.status === 200 || response.status === 204) {
                  alert("Dados do serviço salvos com sucesso!");
                  await this.fetchServices();
                  this.resetForm();
              } else {
                  alert("Erro ao salvar os dados do serviço. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao enviar dados para o backend:", error);
              alert("Erro ao salvar os dados do serviço. Por favor, tente novamente.");
          }
      },

      async fetchServices() {
          try {
              const response = await axios.get('https://localhost:7171/api/MaintenanceServices', {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 200) {
                  this.services = response.data.$values;
              } else {
                  alert("Erro ao buscar os serviços. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao buscar serviços:", error);
              alert("Erro ao buscar os serviços. Por favor, tente novamente.");
          }
      },

      resetForm() {
          this.service = {
              id: null,
              name: "",
              description: "",
          };
          this.editMode = false;
          this.$refs.form.resetValidation();
      },

      setEditService(service) {
          this.service = { ...service };
          this.editMode = true;
      },

      async deleteService(service) {
          try {
              const response = await axios.delete(`https://localhost:7171/api/MaintenanceServices/${service.id}`, {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 204 || response.status === 200) {
                  alert("Serviço excluído com sucesso!");
                  await this.fetchServices();
              } else {
                  alert("Erro ao excluir o serviço. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao excluir o serviço:", error);
              alert("Erro ao excluir o serviço. Por favor, tente novamente.");
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
      this.fetchServices();
  },
};
</script>
