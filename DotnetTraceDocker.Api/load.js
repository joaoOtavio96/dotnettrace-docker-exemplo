import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '20s', target: 300 }
    ],
    thresholds: {
    }
};

export default function () {
    const response = http.get('http://localhost/dosomework');

    check(response, {
        'is status 200': (x) => x.status === 200
    });

    sleep(1);
}