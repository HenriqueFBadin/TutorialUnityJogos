**Por: Henrique Fazzio Badin**

**Baseball Showdown 2D** é um jogo 2D de baseball que tem como objetivo ganhar uma disputa contra um adversário, rebatendo a bola que ele arremessar.

**Sobre o jogo, objetivo, e como jogar:**

No jogo, o jogador começa em um menu principal e acessa um mapa overworld onde controla seu personagem utilizando as teclas **W, A, S, D** ou as setas direcionais. Nesse ambiente, é possível explorar o cenário, coletar bolas e ser desafiado por um inimigo ao entrar em seu campo de visão. Quando isso acontece, o inimigo se move em direção ao jogador e, ao se aproximar, congela seus movimentos e inicia automaticamente o minigame de rebatida. 

Durante o desafio, o jogador utiliza o **cursor do mouse** para posicionar-se na tela e tenta **rebater clicando com o botão esquerdo**. Para que a rebatida seja válida, o cursor precisa estar sobre a bola, e o clique deve ocorrer dentro de um **intervalo de 0.5 segundos antes ou depois da bola atingir seu destino**. Esse intervalo aumenta conforme o tempo total de jogo avança, tornando a janela de rebatida mais permissiva. A bola é lançada automaticamente ao final da música da cena de desafio. Dois tipos de arremesso foram implementados: FastBall, com trajetória reta e rápida, e ChangeUp, que começa veloz e desacelera progressivamente até o destino. A trajetória e seu tipo é escolhida aleatoriamente.

Caso o jogador consiga acertar a bola pelo menos uma vez, ou forçar o arremessador a cometer quatro bolas (arremessos fora da zona de strike sem tentativa de rebatida), antes de ser eliminado por 3 strikes, ele vence o desafio e é levado à tela de finalização. Caso contrário, é redirecionado de volta ao overworld para tentar novamente.

**Referências:**

Visualmente, o jogo utiliza sprites inspirados no jogo Baseball do NES, adaptados para um estilo menos pixelado e coerente com o design geral. O sprite original pode ser encontrado em: The Spriters Resource - NES Baseball. Os backgrounds e tilesets foram criados com o auxílio do ChatGPT, e a bola de baseball utilizada no jogo foi extraída do jogo Sonic Dash, disponível em: The Spriters Resource - Sonic Dash.

Repositório do jogo: https://github.com/HenriqueFBadin/TutorialUnityJogos.git
