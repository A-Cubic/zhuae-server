﻿using ACBC.Common;
using ACBC.Dao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Buss
{
    public class RemoteBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.RemoteApi;
        }

        public object Do_AddMemberInfo(BaseApi baseApi)
        {
            AddMemberInfoParam addMemberInfoParam = JsonConvert.DeserializeObject<AddMemberInfoParam>(baseApi.param.ToString());
            if (addMemberInfoParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            RemoteDao remoteDao = new RemoteDao();
            if(!remoteDao.GetStoreMemberByCode(baseApi.code, addMemberInfoParam.phone))
            {
                throw new ApiException(CodeMessage.RemoteStoreMemberExist, "RemoteStoreMemberExist");
            }

            if(!remoteDao.AddRemoteStoreMember(baseApi.code, addMemberInfoParam.phone, addMemberInfoParam.cardCode, addMemberInfoParam.point))
            {
                throw new ApiException(CodeMessage.AddRemoteStoreMemberError, "AddRemoteStoreMemberError");
            }

            return "";
        }

        public object Do_AddPointRecord(BaseApi baseApi)
        {
            AddPointRecordParam addPointRecordParam = JsonConvert.DeserializeObject<AddPointRecordParam>(baseApi.param.ToString());
            if (addPointRecordParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            RemoteDao remoteDao = new RemoteDao();
            if (!remoteDao.AddPointCommit(baseApi.code, addPointRecordParam.phone, addPointRecordParam.point))
            {
                throw new ApiException(CodeMessage.AddPointCommitError, "AddPointCommitError");
            }

            return "";
        }

        public object Do_GetPointCommitList(BaseApi baseApi)
        {
            RemoteDao remoteDao = new RemoteDao();
            return remoteDao.GetPointCommitList(baseApi.code);
        }

        public object Do_UpdatePointCommit(BaseApi baseApi)
        {
            UpdatePointCommitParam updatePointCommitParam = JsonConvert.DeserializeObject<UpdatePointCommitParam>(baseApi.param.ToString());
            if (updatePointCommitParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            RemoteDao remoteDao = new RemoteDao();
            if (!remoteDao.UpdatePointCommit(updatePointCommitParam.pointCommitId))
            {
                throw new ApiException(CodeMessage.UpdatePointCommitError, "UpdatePointCommitError");
            }

            return "";
        }
    }
}
