import http from 'k6/http';

const url = 'https://localhost:5002/api/';
const username = 'AddUsernameHere';
const password = 'AddPasswordHere';

export const options = {
  // A number specifying the number of VUs to run concurrently.
  vus: 10,
  // A string specifying the total duration of the test run.
  duration: '60s'
};

export function generateGUID() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
};

export function generateRandomString(length) {
  const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let result = '';
  const charactersLength = characters.length;
  for (let i = 0; i < length; i++) {
    result += characters.charAt(Math.floor(Math.random() * charactersLength));
  }
  return result;
};

export default function () {

  let loginData = { username: username, password: password };

  let tokenResponse = http.post(url + 'users/login', JSON.stringify(loginData), {
    headers: { 'Content-Type': 'application/json' },
  });

  let newTask = { id: generateGUID(), description: generateRandomString(10), username: username };

  const params = {
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + tokenResponse.body
    },
  };

  http.post(url + 'tasks', JSON.stringify(newTask), params);

  http.get(url + 'tasks', params);
}