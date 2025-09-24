import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '5s', target: 10 },  
    { duration: '10s', target: 50 },   
    { duration: '1m', target: 100 }, 
    { duration: '3m', target: 1000 },   
    { duration: '30s', target: 5000 },  
    { duration: '2m', target: 5000 }, 
      { duration: '1m', target: 10000 },
      { duration: '2m', target: 500 },   
    { duration: '30s', target: 0}, 
  ],
  thresholds: {
    http_req_failed: ['rate<0.05'],    
    http_req_duration: ['p(95)<1000'], 
  },
};


const locations = [
  '/ru/msk',
  '/ru/svrd ',
  '/ru/svrd/revda',
  '/ru',
];

export default function () {
  
  const randomLocation = locations[Math.floor(Math.random() * locations.length)];
  
  
  const url = `http://localhost:8080/Get?location=${encodeURIComponent(randomLocation)}`;
  
  const params = {
    headers: {
      'Content-Type': 'application/json',
    },
    tags: {
      location: randomLocation, 
    },
  };

  const response = http.get(url, params);

  // Проверки ответа
  check(response, {
    'status is 200': (r) => r.status === 200,
    'response time OK': (r) => r.timings.duration < 2000,
    'valid JSON response': (r) => {
    try {
      const data = JSON.parse(r.body);
      return Array.isArray(data); 
    } catch {
      return false;
    }
  },
    'has response body': (r) => r.body.length > 0,
  });

  sleep(1); 
}