import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import authHeader from "../auth/auth-header";
import CustomerService from "../customer/customer.service";

const API_URL = process.env.REACT_APP_API_URL + "addresses";

const setEmptyAddressToLocalStorage = () => {
    localStorage.setItem(
      "@Address",
      JSON.stringify({
        addressId: "",
        country: "",
        state: "",
        city: "",
        postalCode: "",
        street: "",
        streetNumber: "",
        apartment: "",
      })
    );
  };

  async function setEmptyAddressToLocalStorageAsync() {
    await AsyncStorage.setItem(
      "@Address",
      JSON.stringify({
        addressId: "",
        country: "",
        state: "",
        city: "",
        postalCode: "",
        street: "",
        streetNumber: "",
        apartment: "",
      })
    );
  }

  const GetStoredAddress = () => {
    var storedAddress = localStorage.getItem("@Address");
    if (storedAddress) {
      return JSON.parse(localStorage.getItem("@Address"));
    } else {
      setEmptyAddressToLocalStorage();
      return JSON.parse(localStorage.getItem("@Address"));
    }
  };

  async function GetStoredAddressAsync() {
    var storedAddress = await AsyncStorage.getItem("@Address");
    if (storedAddress) {
      return JSON.parse(storedAddress);
    } else {
      await setEmptyAddressToLocalStorageAsync();
      return JSON.parse(await AsyncStorage.getItem("@Address"));
    }
  }

  const SetStoredAddress = (
    addressId,
    country,
    state,
    city,
    postalCode,
    street,
    streetNumber,
    apartment,
  ) => {
    localStorage.setItem(
      "@Address",
      JSON.stringify({
        addressId: addressId,
        country: country,
        state: state,
        city: city,
        postalCode: postalCode,
        street: street,
        streetNumber: streetNumber,
        apartment: apartment,
      })
    );
  };

  async function SetStoredAddressAsync(
    addressId,
    country,
    state,
    city,
    postalCode,
    street,
    streetNumber,
    apartment,
  ) {
    await AsyncStorage.setItem(
      "@Address",
      JSON.stringify({
        addressId: addressId,
        country: country,
        state:state,
        city: city,
        postalCode: postalCode,
        street: street,
        streetNumber: streetNumber,
        apartment: apartment,
      })
    );
  }

  async function GetAddressAsync() {
    await axios.get(API_URL, { headers: authHeader() }).then((response) => {
      if (response.data)
        AsyncStorage.setItem("@Address", JSON.stringify(response.data));
      return response;
    });
  }

  async function CreateAddressAsync() {
    const customer = await CustomerService.GetStoredCustomerAsync();
    const address = await GetStoredAddressAsync();
    return axios
      .post(
        API_URL,
        {
            Id: customer.customerId,
            Country: address.country,
            State: address.state,
            City: address.city,
            PostalCode: address.postalCode,
            Street: address.street,
            StreetNumber: address.streetNumber,
            Apartment: address.apartment,
        },
        { headers: authHeader() }
      )
      .then((response) => {
        if (response.data) {
          SetStoredAddressAsync(
            customer.customerId,
            address.country,
            address.state,
            address.city,
            address.postalCode,
            address.street,
            address.streetNumber,
            address.apartment,
          );
        }
      });
  }

  const AddressService = {
    GetStoredAddress,
    GetStoredAddressAsync,
    GetAddressAsync,
    CreateAddressAsync,
    SetStoredAddress,
    SetStoredAddressAsync,
  };
  
  export default AddressService;
  