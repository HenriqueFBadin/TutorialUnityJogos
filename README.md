**Por: Henrique Fazzio Badin**

**Link do itch.io do jogo:** https://henriquefbadin.itch.io/tutorial-jogos-henriquefbadin

**Baseball Showdown 2D** é um jogo 2D de baseball que tem como objetivo ganhar uma disputa contra um adversário, rebatendo a bola que ele arremessar.

**Sobre o jogo, objetivo, e como jogar:**

No jogo, o jogador começa em um menu principal e acessa um mapa overworld onde controla seu personagem utilizando as teclas **W, A, S, D** ou as setas direcionais. Nesse ambiente, é possível explorar o cenário, coletar bolas e ser desafiado por um inimigo ao entrar em seu campo de visão. Quando isso acontece, o inimigo se move em direção ao jogador e, ao se aproximar, congela seus movimentos e inicia automaticamente o minigame de rebatida. 

Durante o desafio, o jogador utiliza o **cursor do mouse** para posicionar-se na tela e tenta **rebater clicando com o botão esquerdo**. Para que a rebatida seja válida, o cursor precisa estar sobre a bola, e o clique deve ocorrer dentro de um **intervalo de 0.5 segundos antes ou depois da bola atingir seu destino**. Esse intervalo aumenta conforme o tempo total de jogo avança, tornando a janela de rebatida mais permissiva. A bola é lançada automaticamente **após 6 segundos** da cena de desafio. Dois tipos de arremesso foram implementados: FastBall, com trajetória reta e rápida, e ChangeUp, que começa veloz e desacelera progressivamente até o destino. A trajetória e seu tipo é escolhida aleatoriamente.

Caso o jogador consiga acertar a bola pelo menos uma vez, ou forçar o arremessador a cometer quatro bolas (arremessos fora da zona de strike sem tentativa de rebatida), antes de ser eliminado por 3 strikes, ele vence o desafio e é levado à tela de finalização. Caso contrário, é redirecionado de volta ao overworld para tentar novamente.

**Referências:**

Visualmente, o jogo utiliza sprites inspirados no jogo Baseball do NES, adaptados para um estilo menos pixelado e coerente com o design geral. O sprite original pode ser encontrado em: The Spriters Resource - NES Baseball. Os backgrounds e tilesets foram criados com o auxílio do ChatGPT, e a bola de baseball utilizada no jogo foi extraída do jogo Sonic Dash, disponível em: The Spriters Resource - Sonic Dash.
A lógica de movimentação, detecção e transição de cenas foi baseada em exemplos e discussões da comunidade Unity, como os tópicos "how to make an object move to another object’s location?" e "how to stop player from moving?" disponíveis no Unity Discussions. Algumas funcionalidades do jogo, como o uso de coroutines para lidar com acertos (Hit), foram implementadas com base em sugestões fornecidas pelo ChatGPT e posteriormente foram utilizadas como base para outros métodos.


**Pontos abordados na Rubrica**

1. Entregar o tutorial: C (400 Pontos) ✅
   
2. Conquistando + pontos:
   
  - 2.1. Mecânica de Tempo (200 Pontos):

      - 2.1.1. Adicionar um cronômetro ✅
    
      - 2.1.2. Mostrar o tempo final ao término da partida ✅
    
      - 2.1.3. Utilizar o tempo como mecânica dentro do jogo ✅
        
  - 2.2. Inimigos (150 Pontos):

      - 2.2.1. Implementar inimigos que se movam em direções pré-determinadas ou estejam parados, mas que causem algum impacto ao jogador Fazer com que os inimigos sigam o jogador ou tenham um comportamento variável (patrulha, perseguição). ✅
        
  - 2.3. Visual: (75 Pontos)

      - 2.3.1. Trocar a cor dos sprites básicos ✅
        
      - 2.3.2. Usar assets de sprites de forma coerente ✅

  - 2.4. Audio: (100 Pontos)
    
      - 2.4.1. Adicionar música ao seu jogo ✅
        
      - 2.4.2. Adicionar diferentes elementos sonoros  ✅
        
  - 2.5. UI: (100 Pontos)
    
      - 2.5.1. Exibir o score do jogador e o tempo atual na interface durante o jogo. ✅
        
      - 2.5.2. Mostrar outras informações importantes para o jogador (Vida, Stamina, etc) ✅

  - 2.6. Controles: (50 Pontos)

      - 2.6.1. Adicionar suporte a joystick/controle xbox ✖️

  - 2.7. Level Design: (150 Pontos)
    
      - 2.7.1. Ajustar o jogo a algum tema ✅
        
      - 2.7.2. Criar uma nova arena ou modificar a arena básica ✅
        
      - 2.7.3. Adicionar novos desafios para o jogador ✅

