import React, { useState, useEffect, useCallback } from "react";
import { throttle } from "../utils";

export function TranslatorManagement() {
  const [filter, setFilter] = useState("");
  const [translators, setTranslators] = useState([]);
  const [loadingCounter, setLoadingCounter] = useState(0);
  const [translatorFormStatus, setTranslatorFormStatus] = useState("Applicant");

  const throttledFetchTranslators = useCallback(throttle(fetchTranslators, 2000), []);
  const increaseLoadingCounter = useCallback(() => setLoadingCounter(count => count + 1), []);
  const decreaseLoadingCounter = useCallback(() => setLoadingCounter(count => count - 1), []);

  async function fetchTranslators(filter = "") {
    increaseLoadingCounter();
    const response = await fetch(`/api/translators?name=${filter}`, {
      method: "GET",
    });

    const data = await response.json();
    setTranslators(data);
    decreaseLoadingCounter();
  }

  async function addTranslator(formData) {
    increaseLoadingCounter();
    const response = await fetch(`/api/translators`, {
      method: "POST",
      body: formData,
    });

    if (!response.ok) {
      const data = await response.json();
      alert(data.title);
    }

    await fetchTranslators(filter);
    decreaseLoadingCounter();
  }

  async function setTranslatorStatus(translatorId, newStatus) {
    increaseLoadingCounter();

    const response = await fetch(`/api/translators/${translatorId}`, {
      method: "PUT",
      body: JSON.stringify(newStatus),
      headers: new Headers({
        "Content-Type": "application/json; charset=UTF-8"
      })
    });

    await fetchTranslators(filter);
    decreaseLoadingCounter();
  }

  async function handleSubmit(event) {
    event.preventDefault();
    const formData = new FormData(event.target);
    formData.set("Status", translatorFormStatus);

    await addTranslator(formData);
  }

  function handleFilterChange(event) {
    const value = event.target.value;
    setFilter(value);
    throttledFetchTranslators(value);
  }

  useEffect(() => {
    fetchTranslators();
  }, []);

  let contents = loadingCounter
    ? <p><em>Loading...</em></p>
    : <TranslatorTable translators={translators} setTranslatorStatus={setTranslatorStatus} />;

  return (
    <div>
      <h1 id="tableLabel">Translator Management Page</h1>
      <button className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Add Translator</button>
      <div>
        <label>Filter:</label>
        <input type="text" className="form-control" onChange={handleFilterChange} />
      </div>
      {contents}

      <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div className="modal-dialog">
          <div className="modal-content">
            <form onSubmit={handleSubmit}>
              <div className="modal-header">
                <h1 className="modal-title fs-5" id="exampleModalLabel">New message</h1>
                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div className="modal-body">

                <div className="mb-3">
                  <label htmlFor="translator-name" className="col-form-label">Name:</label>
                  <input type="text" className="form-control" id="translator-name" name="Name" />
                </div>
                <div className="mb-3">
                  <label htmlFor="translator-hourlyrate" className="col-form-label">Hourly Rate:</label>
                  <input type="text" className="form-control" id="translator-hourlyrate" name="HourlyRate" />
                </div>
                <div className="mb-3">
                  <label htmlFor="translator-status" className="col-form-label">Status:</label>
                  <input type="text" className="invisible" name="Status" />
                  <div className="dropdown">
                    <button id="translator-status" className="btn btn-sm btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                      {translatorFormStatus}
                    </button>
                    <ul className="dropdown-menu">
                      {["Applicant", "Certified", "Deleted"].map(status => (
                        <li key={status}>
                          <button type="button" className="dropdown-item" onClick={() => setTranslatorFormStatus(status)}>
                            {status}
                          </button>
                        </li>
                      ))}
                    </ul>
                  </div>
                </div>
                <div className="mb-3">
                  <label htmlFor="translator-creditcardnumber" className="col-form-label">Credit Card Number:</label>
                  <input type="text" className="form-control" id="translator-creditcardnumber" name="CreditCardNumber" />
                </div>
              </div>
              <div className="modal-footer">
                <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="submit" className="btn btn-primary" data-bs-dismiss="modal">Add</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

function TranslatorTable({ translators, setTranslatorStatus }) {
  return (
    <table className="table table-striped" aria-labelledby="tableLabel">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Hourly Rate</th>
          <th>Status</th>
          <th>Credit Card Number</th>
        </tr>
      </thead>
      <tbody>
        {translators.map(translator =>
          <tr key={translator.id}>
            <td>{translator.id}</td>
            <td>{translator.name}</td>
            <td>{translator.hourlyRate}</td>
            <td>
              <div className="dropdown">
                <button className="btn btn-sm btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                  {translator.status}
                </button>
                <ul className="dropdown-menu">
                  {["Applicant", "Certified", "Deleted"].map(status => (
                    <li key={status}>
                      <button className="dropdown-item" onClick={() => setTranslatorStatus(translator.id, status)}>
                        {status}
                      </button>
                    </li>
                  ))}
                </ul>
              </div>
            </td>
            <td>{translator.creditCardNumber}</td>
          </tr>
        )}
      </tbody>
    </table>
  );
}
