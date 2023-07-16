import {Alert, Button, Grid, Snackbar, TextField} from '@mui/material';
import React, {useState} from 'react';
import SaveIcon from '@mui/icons-material/Save';
import {severity as severityEnum} from '../constants/severity'
import {
    emptyMessage,
    requiredTvShowNameMessage,
} from "../constants/message";
import {saveTvShowsUri} from "../constants/uri";

const Home = () => {
    const [severity, setSeverity] = useState(severityEnum[1]);
    const [message, setMessage] = useState(emptyMessage);
    const [openSnackbar, setOpenSnackbar] = useState(false);
    const [showName, setShowName] = useState('');
    const [requiredError, setRequiredError] = useState(false);
    const [requiredHelperText, setRequiredHelperText] = useState(emptyMessage);

    const handleCloseSnackbar = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        setOpenSnackbar(false);
    };

    const handleSaveClick = async (_) => {
        if (showName.length === 0) {
            setRequiredError(true);
            setRequiredHelperText(requiredTvShowNameMessage)
            
            return;
        }
        await saveTvShowWithName();
    }

    const handleShowNameChange = (e) => {
        const currentTarget = e.currentTarget.value;
        if (currentTarget.length > 0) {
            setRequiredError(false);
            setRequiredHelperText(emptyMessage)
        }
        
        setShowName(currentTarget);
    }

    const saveTvShowWithName = async () => {
        fetch(`${saveTvShowsUri}/${showName}`, {
            method: "POST",
            headers: {
                "content-type": "application/json"
            },
            mode: "cors"
        }).then(async (response) => {
            if (!response.ok) {
                return response.json().then(error => {
                    throw error;
                });
            }
            
            return response.json();
        }).then((response) => {
            setSeverity(severityEnum[response?.severity]);
            setMessage(response?.message);
            setOpenSnackbar(true);
        }).catch((error) => {
            setSeverity(severityEnum[2]);
            setMessage(error.Message);
            setOpenSnackbar(true);
        });
    }
    
    return (
        <Grid container spacing={2}>
            <Snackbar
                open={openSnackbar}
                autoHideDuration={6000}
                onClose={handleCloseSnackbar}
                anchorOrigin={{vertical: 'top', horizontal: 'right'}}
            >
                <Alert
                    onClose={(e) => handleCloseSnackbar(e)}
                    severity={severity}
                    sx={{width: '100%'}}
                >
                    {message}
                </Alert>
            </Snackbar>
            <Grid item xs={8}>
                <iframe src="https://giphy.com/embed/2XflxzDAw5pn6WaA372" width="480" height="360" frameBorder="0"
                        className="giphy-embed" allowFullScreen></iframe>
                <p><a href="https://giphy.com/gifs/giffffr-2XflxzDAw5pn6WaA372">via GIPHY</a></p>
            </Grid>
            <Grid item xs={4}></Grid>
            <Grid item xs={3}>
                <TextField
                    id="show-name-id"
                    required
                    label="Tv Show Name"
                    error={requiredError}
                    value={showName}
                    helperText={requiredHelperText}
                    type="search"
                    size="small"
                    fullWidth
                    onChange={handleShowNameChange}
                />
            </Grid>
            <Grid item xs={3}>
                <Button
                    onClick={handleSaveClick}
                    variant="contained"
                    endIcon={<SaveIcon/>}
                    size="medium"
                >
                    Save
                </Button>
            </Grid>
            <Grid item xs={6}></Grid>
        </Grid>
    );
}

export default Home;
