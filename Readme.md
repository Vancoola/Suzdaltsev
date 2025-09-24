из корня проекта билд с помощью Docker: \
`docker build -t suzdaltsev . -f Suzdaltsev.Api/Dockerfile` 

потом запуск:\
`docker run -p 8080:8080  suzdaltsev`

По `http://localhost:8080/` доступен swaggerUI

Проводил нагрузочный тест с 10000 VU, вот результаты (скрипт приложен к проекту):


     execution: local
        script: k6.js
        output: -

     scenarios: (100.00%) 1 scenario, 10000 max VUs, 10m45s max duration (incl. graceful stop):
              * default: Up to 10000 looping VUs for 10m15s over 9 stages (gracefulRampDown: 30s, gracefulStop: 30s)



█ THRESHOLDS

    http_req_duration
    ✓ 'p(95)<1000' p(95)=4.27ms

    http_req_failed
    ✓ 'rate<0.05' rate=0.00%


█ TOTAL RESULTS

    checks_total.......: 7530744 12235.418958/s
    checks_succeeded...: 100.00% 7530744 out of 7530744
    checks_failed......: 0.00%   0 out of 7530744

    ✓ status is 200
    ✓ response time OK
    ✓ valid JSON response
    ✓ has response body

    HTTP
    http_req_duration..............: avg=1.55ms min=145µs med=1.08ms max=52.12ms p(90)=2.99ms p(95)=4.27ms
      { expected_response:true }...: avg=1.55ms min=145µs med=1.08ms max=52.12ms p(90)=2.99ms p(95)=4.27ms
    http_req_failed................: 0.00%   0 out of 1882686
    http_reqs......................: 1882686 3058.85474/s

    EXECUTION
    iteration_duration.............: avg=1s     min=1s    med=1s     max=1.05s   p(90)=1s     p(95)=1s    
    iterations.....................: 1882686 3058.85474/s
    vus............................: 13      min=2            max=9983 
    vus_max........................: 10000   min=10000        max=10000

    NETWORK
    data_received..................: 411 MB  668 kB/s
    data_sent......................: 241 MB  391 kB/s




running (10m15.5s), 00000/10000 VUs, 1882686 complete and 0 interrupted iterations
default ✓ [======================================] 00000/10000 VUs  10m15s