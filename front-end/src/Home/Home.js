import React, { Component } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Modal from "../Modal/Modal";
import {Link} from "react-router-dom";

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: [],
            error: null
        };
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData = () => {
        fetch('https://localhost:7094/api/UrlInfo')
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then((data) => {
                this.setState({ data });
            })
            .catch((error) => {
                console.error('There was a problem with the fetch operation:', error);
                this.setState({ error: 'Error while fetching data' });
            });
    };

    handleDelete = async (id) => {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch(`https://localhost:7094/api/UrlInfo/${id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Failed to delete UrlInfo');
            }

            // Оновлюємо дані після видалення
            this.setState((prevState) => ({
                data: prevState.data.filter(item => item.id !== id)
            }));

            console.log('UrlInfo deleted successfully');
        } catch (error) {
            console.error('Error:', error);
        }
    };

    handleEdit = (id) => {
        console.log('Edit', id);
    };

    handleInfo = (id) => {
        console.log('Info', id);
    };

    handleAddUrl = async (longUrl) => {
        try {
            const token = localStorage.getItem('token');

            if (!token) {
                throw new Error('Authorization token is missing');
            }

            if (!longUrl) {
                throw new Error('URL cannot be empty');
            }

            const response = await fetch('https://localhost:7094/api/UrlInfo/', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ longUrl })
            });
            
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Failed to add UrlInfo');
            }
            
            const responseText = await response.text();
            console.log('Response from server:', responseText);
            if (!responseText) {
                throw new Error('Empty response from server');
            }

            const newUrlInfo = JSON.parse(responseText); 
            console.log('UrlInfo added successfully:', newUrlInfo);

            this.setState((prevState) => ({
                data: [...prevState.data, newUrlInfo],
            }));
        } catch (error) {
            console.error('Error:', error);
        }
    };

    toggleModal = () => {
        this.setState((prevState) => ({ isModalOpen: !prevState.isModalOpen }));
    };

    render() {
        const { data, error, isModalOpen } = this.state;

        return (
            <div className="container mt-5">
                <button className="btn btn-primary mb-4" onClick={this.toggleModal}>
                    Додати URL
                </button>
                {error && <div className="alert alert-danger">{error}</div>}
                {data.length ? (
                    <div>
                        <h3 className="my-4">Результати API:</h3>
                        <table className="table table-striped table-bordered">
                            <thead className="thead-dark">
                            <tr>
                                <th>№</th>
                                <th>Original Url</th>
                                <th>Short URL</th>
                                <th>Owner</th>
                                <th>Date</th>
                                <th>Options</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((urlInfo, index) => (
                                <tr key={urlInfo.id || index}>
                                    <td>{index + 1}</td>
                                    <td>{urlInfo.longUrl}</td>
                                    <td>{urlInfo.shortUrl}</td>
                                    <td>{urlInfo.createdBy}</td>
                                    <td>{new Date(urlInfo.createdDate).toLocaleString()}</td>
                                    <td>
                                        <Link to={`/info/${urlInfo.id}`}>
                                            <button className="btn btn-info btn-sm me-2">Info</button>
                                        </Link>
                                        <button className="btn btn-warning btn-sm me-2"
                                                onClick={() => this.handleEdit(urlInfo.id)}>Edit
                                        </button>
                                        <button className="btn btn-danger btn-sm"
                                                onClick={() => this.handleDelete(urlInfo.id)}>Delete
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            </tbody>

                        </table>
                    </div>
                ) : (
                    <p>Loading...</p>
                )}
                <Modal
                    isOpen={isModalOpen}
                    onClose={this.toggleModal}
                    onSubmit={this.handleAddUrl}
                />
            </div>
        );
    }
}
