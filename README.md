# Projeto de Antecipação 

## 📌 Visão Geral
Este projeto permite que empresas realizem solicitações de antecipação de valores de notas fiscais. O fluxo de movimentação de dados ocorre por meio de APIs, garantindo uma comunicação eficiente entre o front-end e o back-end.

## 🚀 Tecnologias Utilizadas
- Back-end: .NET Core  
- Front-end: JavaScript, HTML, CSS  
- Banco de Dados: SQL Server  
- ORM: Entity Framework  

## 📂 Estrutura do Projeto
### 🔹 Back-end (.NET Core)  
- Desenvolvido com .NET Core e Entity Framework para a manipulação do banco de dados.    
- Disponibiliza APIs REST para a comunicação com o front-end.   
- Realiza os cálculos de antecipação e retorna os resultados em formato JSON.    
### 🔹 Front-end (JS, HTML, CSS)  
-  Interface simples e objetiva para cadastro de empresa, inserção de notas fiscais e solicitação de antecipação.  
-  Comunicação com o back-end via fetch API ou outra biblioteca de requisições HTTP.  
-  Exibição dos cálculos de antecipação em um modal após a seleção das notas fiscais.

  ## ⚙ Instalação e Uso
###  1️⃣ Configuração Inicial
  -  Conecte o banco de dados ao projeto.  
  -  Inicie o projeto.  
### 2️⃣ Cadastro da Empresa  
  -  Na primeira tela, insira as informações da empresa.  (CNPJ, Nome, Faturamento Mensal, Ramo (obrigatório, “Serviços” ou “Produtos”))
### 3️⃣ Cadastro de Notas Fiscais  
  -  Após cadastrar a empresa, insira as notas fiscais no sistema.  
### 4️⃣ Seleção e Cálculo da Antecipação
  -  Selecione as notas fiscais desejadas.  
  -  Adicione as notas fiscais ao carrinho.  
  -  Clique em "Calcular".  
  -  O sistema processa os valores e retorna um JSON com os resultados.
### 5️⃣ Exibição dos Resultados
  -  O JSON retornado pelo back-end é exibido em um modal, contendo os detalhes do cálculo.
## 📝 Considerações Finais
  -  Todos os cálculos são processados no back-end.
