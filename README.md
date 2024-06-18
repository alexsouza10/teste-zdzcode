# Projeto de Manutenção de Veículos

Este projeto é um teste técnico, desenvolvido com um back-end em .NET Core e um front-end em Vue.js utilizando Vuetify.

## Fluxograma do Projeto

<img src="vehicle-maintenance-front/public/fluxograma.png" alt="Guia" style="width: 400px; height: auto;">

## Front-end

### Instalação

1. Certifique-se de ter o Node.js instalado em sua máquina.
2. Clone este repositório: `git clone <URL_DO_REPOSITÓRIO>`
3. Navegue até o diretório do projeto front-end: `cd vehicle-maintenance-front`
4. Instale as dependências: `npm install`

### Uso

1. Para iniciar o servidor de desenvolvimento local, execute: `npm run dev`
2. Abra seu navegador e acesse `http://localhost:3000` para visualizar o aplicativo.

### Principais Tecnologias Utilizadas

- Vue.js
- Vuetify
- Nuxt.js (Framework Vue.js)
- Axios (Cliente HTTP)

### Estrutura do Projeto

- **/pages**: Contém as páginas Vue.js do aplicativo.
- **/components**: Componentes reutilizáveis do Vue.js.
- **/layouts**: Layouts para as páginas.
- **/assets**: Arquivos estáticos, como imagens e ícones.

## Back-end

### Instalação

1. Certifique-se de ter o SDK do .NET Core instalado em sua máquina.
2. Certifique-se de ter o PostgreSQL instalado e em execução em sua máquina.
3. Clone este repositório: `git clone <URL_DO_REPOSITÓRIO>`
4. Navegue até o diretório do projeto back-end: `cd vehicle-maintenance-back`
5. Altere o usuário e a senha do banco de dados no arquivo de configuração `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=vehicle_maintenance;Username=seu_usuario;Password=sua_senha"
      }
    }
    ```
6. Execute o projeto: `dotnet run`

### Uso

O back-end será iniciado e estará ouvindo as requisições na porta 5146. Certifique-se de que o front-end está iniciado para fazer as requisições para essa porta.

### Principais Tecnologias Utilizadas

- .NET Core
- Entity Framework Core
- Swagger (Documentação da API)

### Estrutura do Projeto

- **/Controller**: Controladores da API.
- **/Model**: Modelos de dados.
- **/Service**: Lógica de negócios.
- **/Migrations**: Migrações do banco de dados.
- **/Properties**: Propriedades do projeto.
- **/appsettings.json**: Configurações do aplicativo.

## Contribuição

Sinta-se à vontade para contribuir com este projeto. Basta seguir estas etapas:

1. Fork este repositório.
2. Crie um novo branch: `git checkout -b feature/nova-funcionalidade`
3. Faça suas alterações e adicione-as: `git add .`
4. Faça um commit das suas alterações: `git commit -m 'Adiciona nova funcionalidade'`
5. Envie para o branch principal: `git push origin feature/nova-funcionalidade`
6. Crie um novo pull request.

## Telas do Projeto

<table>
  <tr>
    <td><img src="vehicle-maintenance-front/public/welcome.png" alt="Bem-vindo" style="width: 400px; height: auto;"></td>
    <td><img src="vehicle-maintenance-front/public/RegisterError.png" alt="Register-Error" style="width: 400px; height: auto;"></td>
  </tr>
  <tr>
    <td><img src="vehicle-maintenance-front/public/RegisterSave.png" alt="Register-Save" style="width: 400px; height: auto;"></td>
    <td><img src="vehicle-maintenance-front/public/APIbackend.png" alt="API" style="width: 400px; height: auto;"></td>
  </tr>
</table>



## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
