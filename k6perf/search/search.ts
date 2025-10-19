import http from 'k6/http';
import { check, sleep } from 'k6';

const targetVUs = __ENV.TARGET_VUS ? parseInt(__ENV.TARGET_VUS) : 10;
const mainDuration = __ENV.DURATION || '30s';

export const options = {
  stages: [
    { duration: '10s', target: Math.max(1, Math.floor(targetVUs / 3)) }, 
    { duration: '10s', target: targetVUs }, 
    { duration: mainDuration, target: targetVUs }, 
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<1000', 'p(90)<500'],
    http_req_failed: ['rate<0.01'],
  }
};

const baseUrl = __ENV.BASE_URL || '...';
const authToken = __ENV.AUTH_TOKEN || '...';


const sampleFirstNames = ['Ros', 'Rick', 'Joh', 'Mar', 'Xz', 'Ded', 'Mer', 'Lol', 'Jeff', 'Ale', 'Felix', 'Max', 'Sean', 'Bla', 'Brin' ];
const sampleLastNames = ['Scm', 'Zal', 'Harv', 'Kris', 'Hell', 'Schow', 'Litt', 'Sca', 'Swift', 'Man', 'Flex', 'Gran', 'Gid', 'Good'];

function pickRandom<T>(arr: T[]): T {
  return arr[Math.floor(Math.random() * arr.length)];
}


function calcSleep(): number {
    const mode = (__ENV.MODE || 'realistic').toLowerCase();

    if (mode === 'stress') {
        return 0.01; 
    }

    return 0.5 + Math.random();
}

export default function (): void {
  const firstName = pickRandom(sampleFirstNames);
  const lastName = pickRandom(sampleLastNames);

  const url = `${baseUrl}/api/v1/user/search?firstName=${encodeURIComponent(firstName)}&lastName=${encodeURIComponent(lastName)}`;

  const res = http.get(url, {
    headers: {
      Authorization: `Bearer ${authToken}`
    },
  });

  check(res, { 'status is 200': (r) => r.status === 200 });

  sleep(calcSleep());
}