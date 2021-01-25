# Alien_Invasion_II

## Cuestiones importantes para el uso

Para comenzar el juego tienes que acceder tras la pantalla de inicio dándole a Start, una vez hecho esto el jugador se moverá en línea recta solo cuando mueva la cámara  30º hacia abajo, haciendo que se desplace en dicha dirección y no pudiendo salir de un espacio determinado en el mapa. El juego está basado en un sistema de rondas en el cual por cada una va aumentando la dificultad al matar los enemigos, así como el número de los que salen. Al matar un enemigo este nos dará cierta cantidad de oro, dándonos acceso a comprar diferentes mejoras en una tienda. El jugador además poseerá dos armas, una metralleta y una pistola, la cual tiene una cantidad de munición, del cargador y de daño determinada; pudiendo el jugador cambiar de arma al darle al icono de este en una UI. Cada vez que el usuario reciba un disparo perderá una cantidad de vida, pudiendo este morir y pasar a la pantalla de Game Over.
  
## Hitos de programación logrados relacionándolos con los contenidos que se han impartido.

Hemos logrado la utilización de temas como son el uso de CardBoard VR para la correcta utilización en el móvil, dándonos así la opción de mover la cámara a través del giroscopio; utilización de mallas para el caso de que el jugador sea disparado; utilización de Event System para acciones como poder detectar el disparo hacia los enemigos; uso de Colliders para detectar cuando llega un disparo del enemigo y de esta forma bajar la vida del jugador.

Tenemos también una UI adaptada a las necesidades de la realidad virtual, datos importantes sobre el juego como la vida del jugador o la puntuación que ha conseguido, se muestra directamente en el mundo virtual. Aparte de esto, el jugador tiene una tienda disponible en el que puede gastar dinero en mejoras para el personaje, simplemente apuntando a un elemento del mundo virtual para comprarlo.

En los enemigos se han utilizado corrutinas en varios apartados, uno para manejar el sistema de aparición de los enemigos con sus temporizadores entre ronda y también en los propias naves enemigas para proporcionar un sistema de balanceo para que parezca que están flotando. También se han usado delegados con el jefe final, este, hace aparecer dos naves enemigas y una vez el jefe es destruido, se envía un evento delegado a dichas naves y se destruyen.

## Aspectos que destacarías en la aplicación. Especificar si se han incluido sensores de los que se han trabajado en interfaces multimodales.

Destacaría el uso de herramientas como son Game Controller, dándonos la posibilidad de mover correctamente al jugador; de Event System Trigger, los cuales nos han sido de gran importancia a la hora de disparar al enemigo; de funciones de Corrutina, las cuales nos han sido de gran ayuda en temas como pueden ser recargar el arma, manejar las rondas de enemigos y su aumento de dificultad.

El cambio entre escenas para ir moviéndonos entre el menú principal, el juego y la pantalla de Game Over, son elementos importantes que nos permiten hacer cosas básicas como iniciar el juego o ver la puntuación final de nuestra partida. Dicha puntuación se almacena entre cambios de escena y es mostrada en una lista de las 3 mejores puntuaciones conseguidas en la escena de derrota.

También es digno de mencionar que se ha usado el componente de animación para las naves espaciales generadas por el jefe final, estableciendo dos circuitos diferentes que recorrerán cada nave.

## Gifs animados mostrando la ejecución del prototipo:

### Cambiando de Arma:

![CambiarArma](./gif/CambiarArma.gif)

### Movimiento del personaje:

![Movimiento](./gif/Movimiento.gif)

### Disparando a los Enemigos:

![Enemigo](./gif/Enemigo.gif)

### Comprando mejoras para el personaje:

![ComprarUpgrade](./gif/ComprarUpgrade.gif)

### Recuperando la vida del personaje:

![RecuperarVida](./gif/RecuperarVida.gif)

### Venciendo a un jefe final:

![VencerBoss](./gif/VencerBoss.gif)

### Pantalla del final del juego (Game Over):

![GameOver](./gif/GameOver.gif)

## Acta de los acuerdos del grupo respecto al trabajo en equipo: reparto de tareas, tareas desarrolladas individualmente, tareas desarrolladas en grupo, etc.
  
Nuestra organización ha sido esta:
1. Alberto: Jugador, Armas
2. Sergio: UI, Escenario, Puntuación, Música, Recompensas de enemigos
3. Cristian: Enemigos
	
Alberto se ha encargado de diseñar la vida, colisión y estadísticas del jugador, también del diseño y asignación de armas, de su correcto funcionamiento y sus sonidos, tanto al disparar, recargar, etc.

Sergio se ha encargado del diseño de todos los escenarios, también de diseñar todo lo relacionado con UI, es decir, menú inicial, tienda, el manejo de esta y la pantalla de Game Over. Por último también se encargó de todo el sistema de puntuación, la música y tanto el manejo del dinero recibido al matar a un enemigo como también la puntuación obtenida de este.

Cristian se ha encargado de todo lo relacionado a los enemigos, su modelo, su aparición, su vida y muerte, su colisión, sus rutas, los disparos y sus daños y por último también el jefe final y sus mecánicas.

Las tareas en las que han sido necesaria la cooperación han sido en los momentos de interacción entre diferentes sistemas, por ejemplo, las armas del jugador y la vida de los enemigos, los disparos de las naves enemigas y el jugador, la organización de cierta información mostrada en la UI que afectaba al temporizador entre rondas y a la información del jugador(vida, puntuación, dinero) y procesos más simples como búsqueda y solución de errores.
