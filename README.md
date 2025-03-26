# Tycoon de Lenhadores - Teste MindGG

## Visão Geral
Este projeto foi desenvolvido como parte do processo seletivo da empresa MindGG. Trata-se de um jogo no estilo tycoon, inspirado em **Magnata Minerador: Gold & Cash** da Kolibri Games, mas criado inteiramente do zero com mecânicas e características originais.

O jogo simula a cadeia produtiva da madeira, onde diferentes trabalhadores automatizados desempenham funções específicas para gerar dinheiro e permitir upgrades contínuos.

## Funcionalidade do Jogo
### Mecânica Principal
O jogador gerencia um sistema automatizado de produção de madeira:
- **Lenhador**: Corta as árvores automaticamente.
- **Transportador**: Leva os troncos cortados de um local para outro.
- **Carpinteiro**: Transforma os troncos em tábuas de madeira para venda.

Com o dinheiro arrecadado, o jogador pode **comprar upgrades infinitos** para os trabalhadores, aumentando sua eficiência e maximizando os lucros.

### Diferenciais Implementados
- **Sistema próprio de notificações** para informar o jogador sobre eventos importantes.
- **Animações de UI** para melhorar a experiência do usuário.
- **Sistema próprio de tempo**, permitindo eventos programados e crescimento dinâmico das árvores.
- **Eventos e Bônus** que aparecem ao longo da jogatina:
  - **Baús de dinheiro**: O jogador pode coletá-los para obter um valor extra.
  - **Chuva**: Faz com que as árvores cresçam instantaneamente, acelerando a produção.

## Tecnologias Utilizadas
- **Engine**: Unity 6000.0.40f1
- **Linguagem**: C#
- **Gerenciamento de Cena e UI**: Unity UI Toolkit & Animator
- **Arte**: Assets gratuitos obtidos na Unity Asset Store e no Itch.io

## Estrutura do Projeto
- **MindggTeste/** → Pasta contendo todos os arquivos do projeto Unity.
- **MindggBuild_windows/** → Pasta contendo o executável do jogo para Windows.

## Instalação e Execução
1. Baixe ou clone este repositório.
2. Para rodar o jogo no Unity:
   - Abra o Unity na versão **6000.0.40f1**.
   - Carregue a pasta **MindggTeste**.
   - Execute a cena principal.
3. Para jogar diretamente no Windows:
   - Acesse a pasta **MindggBuild_windows**.
   - Execute o arquivo `.exe` correspondente.

## Considerações Finais
Este projeto demonstra habilidades na criação de jogos do gênero tycoon, incluindo programação de mecânicas base, otimização de fluxo de jogo, design de UI e implementação de eventos dinâmicos. Qualquer dúvida ou sugestão, sinta-se à vontade para entrar em contato!

---
*Desenvolvido por Abhner Saccomano para o teste da MindGG.*

