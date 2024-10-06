import * as THREE from 'three';

// Setup della scena
const scene = new THREE.Scene();

// Setup della camera
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
camera.position.z = 5;

// Setup del renderer
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Creazione delle stelle
const starGeometry = new THREE.BufferGeometry();
const starMaterial = new THREE.PointsMaterial({ color: 0xffffff });
const starVertices = [];

for (let i = 0; i < 1000; i++) {
  const x = (Math.random() - 0.5) * 200;
  const y = (Math.random() - 0.5) * 200;
  const z = (Math.random() - 0.5) * 200;
  starVertices.push(x, y, z);
}

starGeometry.setAttribute('position', new THREE.Float32BufferAttribute(starVertices, 3));
const stars = new THREE.Points(starGeometry, starMaterial);
scene.add(stars);

// Funzione di animazione
export default function animate() {
  requestAnimationFrame(animate);

  // Movimento delle stelle verso la camera
  stars.position.z += 0.1;

  // Se le stelle hanno superato un certo punto, riportale indietro
  if (stars.position.z > 50) {
    stars.position.z = -50;
  }

  renderer.render(scene, camera);
}


// Gestione del ridimensionamento della finestra
window.addEventListener('resize', () => {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
});
