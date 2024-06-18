<template>
  <v-card>
      <!-- Formulário para adicionar/editar registros de manutenção -->
      <v-container fluid>
          <v-form ref="form" v-model="valid">
              <v-text-field
                  v-model="record.description"
                  :rules="[v => !!v || 'Descrição é obrigatória']"
                  label="Descrição"
                  required
              ></v-text-field>
              
              <v-menu
                  ref="dateMenu"
                  v-model="dateMenu"
                  :close-on-content-click="false"
                  transition="scale-transition"
                  offset-y
                  min-width="auto"
              >
                  <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                          v-model="record.date"
                          label="Data"
                          prepend-icon="mdi-calendar"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                          :rules="[v => !!v || 'Data é obrigatória']"
                          required
                      ></v-text-field>
                  </template>
                  <v-date-picker v-model="record.date" @input="dateMenu = false"></v-date-picker>
              </v-menu>
              
              <v-text-field
                  v-model="record.cost"
                  :rules="[v => !!v || 'Custo é obrigatório']"
                  label="Custo"
                  type="number"
                  required
              ></v-text-field>
              <v-text-field v-if="editMode" v-model="record.id" label="ID" required></v-text-field>

              <v-btn :disabled="!valid" color="primary" class="mr-4" @click="submit">{{ editMode ? 'Salvar Alteração' : 'Salvar' }}</v-btn>
              <v-btn color="error" @click="resetForm">Resetar</v-btn>
              <v-btn color="secondary" @click="fetchRecords">Listar Registros</v-btn>
              <v-btn color="warning" @click="toggleEditMode">{{ editMode ? 'Cancelar Alteração' : 'Alterar Registro' }}</v-btn>
              <!-- Botão para habilitar alteração de registro -->
          </v-form>
      </v-container>

      <!-- Tabela de registros de manutenção -->
      <v-data-table v-if="records.length > 0" :headers="headers" :items="records" :items-per-page="5"
          class="elevation-1">
          <template v-slot:item.actions="{ item }">
              <v-icon small @click="deleteRecord(item)">mdi-delete</v-icon>
          </template>
          <template v-slot:item.id="{ item }">
              {{ item.id }}
          </template>
          <template v-slot:no-data>
              <v-alert :value="true" color="error" icon="mdi-alert">
                  Nenhum registro encontrado.
              </v-alert>
          </template>
      </v-data-table>
      <div v-else>
          <v-alert :value="true" color="info" icon="mdi-information">
              Carregando registros...
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
          dateMenu: false, // Estado para o menu de data
          record: {
              id: null,
              description: "",
              date: "",
              cost: null,
          },
          headers: [
              { text: "ID", value: "id" },
              { text: "Descrição", value: "description" },
              { text: "Data", value: "date" },
              { text: "Custo", value: "cost" },
              { text: 'Actions', value: 'actions', sortable: false },
          ],
          records: [], // Array para armazenar os registros de manutenção da API
          editMode: false, // Modo de edição
      };
  },

  methods: {
      async submit() {
          if (!this.$refs.form.validate()) {
              return;
          }

          try {
              let payload = { ...this.record };
              if (payload.id === null) {
                  delete payload.id;
              } else {
                  payload.id = parseInt(payload.id);
              }

              const method = payload.id ? 'put' : 'post';
              const url = `https://localhost:7171/api/MaintenanceRecords${payload.id ? `/${payload.id}` : ''}`;

              const response = await axios({
                  method,
                  url,
                  data: payload,
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });

              if (response.status === 201 || response.status === 200 || response.status === 204) {
                  alert("Dados do registro salvos com sucesso!");
                  await this.fetchRecords();
                  this.resetForm();
              } else {
                  alert("Erro ao salvar os dados do registro. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao enviar dados para o backend:", error);
              alert("Erro ao salvar os dados do registro. Por favor, tente novamente.");
          }
      },

      async fetchRecords() {
          try {
              const response = await axios.get('https://localhost:7171/api/MaintenanceRecords', {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 200) {
                  this.records = response.data.$values;
              } else {
                  alert("Erro ao buscar os registros. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao buscar registros:", error);
              alert("Erro ao buscar os registros. Por favor, tente novamente.");
          }
      },

      resetForm() {
          this.record = {
              id: null,
              description: "",
              date: "",
              cost: null,
          };
          this.editMode = false;
          this.$refs.form.resetValidation();
      },

      setEditRecord(record) {
          this.record = { ...record };
          this.editMode = true;
      },

      async deleteRecord(record) {
          try {
              const response = await axios.delete(`https://localhost:7171/api/MaintenanceRecords/${record.id}`, {
                  httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
              });
              if (response.status === 204 || response.status === 200) {
                  alert("Registro excluído com sucesso!");
                  await this.fetchRecords();
              } else {
                  alert("Erro ao excluir o registro. Por favor, tente novamente.");
              }
          } catch (error) {
              console.error("Erro ao excluir o registro:", error);
              alert("Erro ao excluir o registro. Por favor, tente novamente.");
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
      this.fetchRecords();
  },
};
</script>
