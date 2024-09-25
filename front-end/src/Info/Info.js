import React, { Component } from 'react';
import { useParams } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

class Info extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: null,
            error: null,
            isLoading: true
        };
    }

    componentDidMount() {
        this.fetchUrlInfo();
    }

    fetchUrlInfo = async () => {
        const { id } = this.props.params;
        try {
            const token = localStorage.getItem('token');

            const response = await fetch(`https://localhost:7094/api/UrlInfo/${id}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error('Failed to fetch UrlInfo');
            }

            const data = await response.json();
            this.setState({ data, isLoading: false });
        } catch (error) {
            this.setState({ error: error.message, isLoading: false });
        }
    };

    render() {
        const { data, error, isLoading } = this.state;

        if (isLoading) {
            return <div className="text-center mt-5"><strong>Loading...</strong></div>;
        }

        if (error) {
            return <div className="alert alert-danger text-center">{error}</div>;
        }

        if (!data) {
            return <div className="alert alert-warning text-center">Data is not available</div>;
        }

        return (
            <div className="container mt-5">
                <h3 className="mb-4 text-center">Info</h3>
                <table className="table table-bordered table-striped table-hover">
                    <thead className="thead-dark">
                    <tr>
                        <th>ID</th>
                        <th>Long URL</th>
                        <th>Short URL</th>
                        <th>Created By</th>
                        <th>Created Date</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr>
                        <td>{data.id}</td>
                        <td>
                            <a href={data.longUrl} target="_blank" rel="noopener noreferrer">
                                {data.longUrl}
                            </a>
                        </td>
                        <td>{data.shortUrl}</td>
                        <td>{data.createdBy}</td>
                        <td>{new Date(data.createdDate).toLocaleString()}</td>
                    </tr>
                    </tbody>
                </table>
            </div>
        );
    }
}

export default (props) => <Info {...props} params={useParams()} />;
