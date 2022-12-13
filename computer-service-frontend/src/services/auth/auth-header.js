import AuthService from "./auth.service";

export default function authHeader() {
  const user = AuthService.getCurrentUser();
  if (user) {
    return { authorization: "Bearer " + user.data.accessToken };
  } else {
    return {};
  }
}