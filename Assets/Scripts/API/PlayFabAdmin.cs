#if ENABLE_PLAYFABADMIN_API && !DISABLE_PLAYFAB_STATIC_API
using PlayFab;
using PlayFab.AdminModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabAdmin : MonoBehaviour
{
    private const string TitleId = "C0CAF"; // Thay đổi với TitleId của bạn
    private const string ApiKey = "PTB5TK17QTTEDYNW3JQ4PZF5AHGC65EKMO17X6X5UJIT9TFJ18"; // Thay đổi với API Key của bạn

    private int startIndex = 0;
    private int maxResults = 100;

    void Start()
    {
        // Thiết lập thông tin PlayFab
        PlayFabSettings.TitleId = TitleId;
        PlayFabSettings.DeveloperSecretKey = ApiKey;

        // Lấy danh sách người chơi
        GetPlayerList();
    }

    // Gọi API để lấy danh sách người chơi
    private void GetPlayerList()
    {
        ListPlayersRequest request = new ListPlayersRequest
        {
            MaxResultsCount = maxResults,
            StartIndex = startIndex
        };

        // Gọi API để lấy danh sách người chơi
        PlayFabAdminAPI.ListPlayers(request, OnListPlayersSuccess, OnListPlayersFailure);
    }

    // Xử lý thành công khi lấy danh sách người chơi
    private void OnListPlayersSuccess(ListPlayersResult result)
    {
        Debug.Log("Players found: " + result.PlayerProfiles.Count);

        // Hiển thị thông tin của các người chơi
        foreach (var player in result.PlayerProfiles)
        {
            Debug.Log($"Player ID: {player.PlayerId}");
            Debug.Log($"Display Name: {player.DisplayName}");
            // Thêm các thông tin khác mà bạn muốn hiển thị
        }

        // Kiểm tra xem còn trang kết quả tiếp theo không
        if (result.PlayerProfiles.Count >= maxResults)
        {
            startIndex += maxResults;
            GetPlayerList(); // Tiếp tục lấy trang kết quả tiếp theo
        }
    }

    // Xử lý lỗi khi gọi API thất bại
    private void OnListPlayersFailure(PlayFabError error)
    {
        Debug.LogError("Error fetching player list: " + error.ErrorMessage);
    }
}
#endif
