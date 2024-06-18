<template>
    <v-card>
        <!-- Formulário para adicionar/editar veículos -->
        <v-container fluid>
            <v-form ref="form" v-model="valid">
                <v-text-field
                    v-model="vehicle.make"
                    :rules="[v => !!v || 'Marca é obrigatória']"
                    label="Marca"
                    required
                ></v-text-field>
                <v-text-field
                    v-model="vehicle.model"
                    :rules="[v => !!v || 'Modelo é obrigatório']"
                    label="Modelo"
                    required
                ></v-text-field>
                <v-text-field
                    v-model="vehicle.year"
                    :rules="[v => !!v || 'Ano é obrigatório', v => /^\d{4}$/.test(v) || 'Ano deve ser um número de 4 dígitos']"
                    label="Ano"
                    required
                ></v-text-field>
                <v-text-field
                    v-model="vehicle.licensePlate"
                    :rules="[v => !!v || 'Placa é obrigatória']"
                    label="Placa"
                    required
                ></v-text-field>
                <v-text-field v-if="editMode" v-model="vehicle.id" label="ID" required></v-text-field>

                <v-btn :disabled="!valid" color="primary" class="mr-4" @click="submit">{{ editMode ? 'Salvar Alteração' : 'Salvar' }}</v-btn>
                <v-btn color="error" @click="resetForm">Resetar</v-btn>
                <v-btn color="secondary" @click="fetchVehicles">Listar Veículos</v-btn>
                <v-btn color="warning" @click="toggleEditMode">{{ editMode ? 'Cancelar Alteração' : 'Alterar Veículo' }}</v-btn>
                <!-- Botão para habilitar alteração de veículo -->
            </v-form>
        </v-container>

        <!-- Tabela de veículos -->
        <v-data-table v-if="vehicles.length > 0" :headers="headers" :items="vehicles" :items-per-page="5"
            class="elevation-1">
            <template v-slot:item.actions="{ item }">
                <v-icon small @click="deleteVehicle(item)">mdi-delete</v-icon>
            </template>
            <template v-slot:item.id="{ item }">
                {{ item.id }}
            </template>
            <template v-slot:no-data>
                <v-alert :value="true" color="error" icon="mdi-alert">
                    Nenhum veículo encontrado.
                </v-alert>
            </template>
        </v-data-table>
        <div v-else>
            <v-alert :value="true" color="info" icon="mdi-information">
                Carregando veículos...
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
            vehicle: {
                id: null,
                make: "",
                model: "",
                year: "",
                licensePlate: "",
            },
            headers: [
                { text: "ID", value: "id" },
                { text: "Marca", value: "make" },
                { text: "Modelo", value: "model" },
                { text: "Ano", value: "year" },
                { text: "Placa", value: "licensePlate" },
                { text: 'Actions', value: 'actions', sortable: false },
            ],
            vehicles: [], // Array para armazenar os veículos da API
            editMode: false, // Modo de edição
        };
    },

    methods: {
        async submit() {
            if (!this.$refs.form.validate()) {
                return;
            }

            try {
                let payload = { ...this.vehicle };
                if (payload.id === null) {
                    delete payload.id;
                } else {
                    payload.id = parseInt(payload.id);
                }

                const method = payload.id ? 'put' : 'post';
                const url = `https://localhost:7171/api/Vehicles${payload.id ? `/${payload.id}` : ''}`;

                const response = await axios({
                    method,
                    url,
                    data: payload,
                    httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
                });

                if (response.status === 201 || response.status === 200 || response.status === 204) {
                    alert("Dados do veículo salvos com sucesso!");
                    await this.fetchVehicles();
                    this.resetForm();
                } else {
                    alert("Erro ao salvar os dados do veículo. Por favor, tente novamente.");
                }
            } catch (error) {
                console.error("Erro ao enviar dados para o backend:", error);
                alert("Erro ao salvar os dados do veículo. Por favor, tente novamente.");
            }
        },

        async fetchVehicles() {
            try {
                const response = await axios.get('https://localhost:7171/api/Vehicles', {
                    httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
                });
                if (response.status === 200) {
                    this.vehicles = response.data.$values;
                } else {
                    alert("Erro ao buscar os veículos. Por favor, tente novamente.");
                }
            } catch (error) {
                console.error("Erro ao buscar veículos:", error);
                alert("Erro ao buscar os veículos. Por favor, tente novamente.");
            }
        },

        resetForm() {
            this.vehicle = {
                id: null,
                make: "",
                model: "",
                year: "",
                licensePlate: "",
            };
            this.editMode = false;
            this.$refs.form.resetValidation();
        },

        setEditVehicle(vehicle) {
            this.vehicle = { ...vehicle };
            this.editMode = true;
        },

        async deleteVehicle(vehicle) {
            try {
                const response = await axios.delete(`https://localhost:7171/api/Vehicles/${vehicle.id}`, {
                    httpsAgent: new https.Agent({ rejectUnauthorized: false }) // Ignorar verificação do certificado SSL
                });
                if (response.status === 204 || response.status === 200) {
                    alert("Veículo excluído com sucesso!");
                    await this.fetchVehicles();
                } else {
                    alert("Erro ao excluir o veículo. Por favor, tente novamente.");
                }
            } catch (error) {
                console.error("Erro ao excluir o veículo:", error);
                alert("Erro ao excluir o veículo. Por favor, tente novamente.");
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
        this.fetchVehicles();
    },
};
</script>
