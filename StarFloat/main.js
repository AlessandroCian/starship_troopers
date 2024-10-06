import './style.css'
import game from './game.js';

document.querySelector('#app').innerHTML = `
  <div>
    <h1 class="button">
        Passa dentro!
        <label  id="counter" class="counter"></label>
    </h1>
    <span  id="speed"></span>
  </div>
`

game();